using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Application.Interfaces;
using CarRentalService.Infrastructure.Kafka.Deserializers;
using Confluent.Kafka;

namespace CarRentalService.Infrastructure.Kafka;

/// <summary>
/// Kafka consumer service for processing rental contracts
/// </summary>
/// <param name="configuration">Application configuration</param>
/// <param name="logger">Logger instance</param>
/// <param name="keyDeserializer">Deserializer for message keys</param>
/// <param name="valueDeserializer">Deserializer for message values</param>
/// <param name="scopeFactory">Dependency injection scope factory</param>
public class KafkaConsumer(
    IConfiguration configuration,
    ILogger<KafkaConsumer> logger,
    KeyDeserializer keyDeserializer,
    ValueDeserializer valueDeserializer,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IConsumer<Guid, IList<RentalRequest>> _consumer = CreateConsumer(
        configuration,
        keyDeserializer,
        valueDeserializer,
        logger);

    private bool _disposed;

    /// <summary>
    /// Main execution method for the consumer
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Kafka consumer started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                if (result is null || result.IsPartitionEOF)
                    continue;

                logger.LogInformation(
                    "Received message {Key} with {Count} rentals (Partition: {Partition}, Offset: {Offset})",
                    result.Message.Key,
                    result.Message.Value.Count,
                    result.Partition,
                    result.Offset);

                await ProcessMessageAsync(result.Message.Key, result.Message.Value);
            }
            catch (ConsumeException ex)
            {
                logger.LogError(ex, "Consume error: {Reason}", ex.Error.Reason);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Kafka consumer cancelled");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error in Kafka consumer");
                await Task.Delay(2000, stoppingToken);
            }
        }

        logger.LogInformation("Kafka consumer stopped");
    }

    private static IConsumer<Guid, IList<RentalRequest>> CreateConsumer(
        IConfiguration configuration,
        KeyDeserializer keyDeserializer,
        ValueDeserializer valueDeserializer,
        ILogger<KafkaConsumer> logger)
    {
        var kafkaConfig = configuration.GetSection("Kafka");
        var topicName = kafkaConfig["Topic"]
            ?? throw new KeyNotFoundException("Kafka topic is not configured");

        var bootstrapServers = configuration.GetConnectionString("car-rental-kafka")
            ?? throw new KeyNotFoundException("Kafka connection string 'car-rental-kafka' is missing");

        var groupId = kafkaConfig["GroupId"]
            ?? throw new KeyNotFoundException("Kafka GroupId is not configured");

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            AllowAutoCreateTopics = true
        };

        var consumer = new ConsumerBuilder<Guid, IList<RentalRequest>>(consumerConfig)
            .SetKeyDeserializer(keyDeserializer)
            .SetValueDeserializer(valueDeserializer)
            .SetErrorHandler((_, e) => logger.LogError("Kafka Error: {Reason}", e.Reason))
            .Build();

        consumer.Subscribe(topicName);
        logger.LogInformation("Subscribed to Kafka topic: {Topic}", topicName);

        return consumer;
    }

    private async Task ProcessMessageAsync(Guid key, IList<RentalRequest>? rentals)
    {
        if (rentals is not { Count: > 0 })
        {
            logger.LogWarning("Empty rentals list for message {Key}", key);
            return;
        }

        try
        {
            using var scope = scopeFactory.CreateScope();
            var rentalService = scope.ServiceProvider.GetRequiredService<IRentalService>();

            foreach (var rentalRequest in rentals)
            {
                await rentalService.CreateAsync(rentalRequest);
            }

            logger.LogInformation("Processed message {Key}: {Count} rentals saved", key, rentals.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing message {Key}", key);
        }
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    /// <param name="disposing">True if called from Dispose, false if from finalizer</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _consumer.Close();
                _consumer.Dispose();
            }

            _disposed = true;
        }
    }

    /// <summary>
    /// Cleanup resources on dispose
    /// </summary>
    public override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        base.Dispose();
    }
}