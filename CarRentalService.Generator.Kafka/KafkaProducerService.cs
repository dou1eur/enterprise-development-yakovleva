using CarRentalService.Generator.Kafka.Services;

namespace CarRentalService.Generator.Kafka;

/// <summary>
/// Background service for generating and sending rental contracts to Kafka
/// </summary>
/// <param name="configuration">Application configuration</param>
/// <param name="producerService">Kafka producer service</param>
/// <param name="logger">Logger instance</param>
public class KafkaProducerService(
    IConfiguration configuration,
    IProducerService producerService,
    ILogger<KafkaProducerService> logger) : BackgroundService
{
    private readonly int _batchSize = configuration.GetValue<int>("Generator:BatchSize");
    private readonly int _payloadLimit = configuration.GetValue<int>("Generator:PayloadLimit");
    private readonly int _waitTimeSeconds = configuration.GetValue<int>("Generator:WaitTime");

    /// <summary>
    /// Main execution method for the background service
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ValidateConfiguration();

        logger.LogInformation(
            "Starting generator: {Total} total rentals, {Batch} per batch, {WaitTime}s interval",
            _payloadLimit, _batchSize, _waitTimeSeconds);

        var sentCount = 0;

        while (sentCount < _payloadLimit && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                var rentals = RentalDataGenerator.GenerateRentals(_batchSize);
                await producerService.SendAsync(rentals);

                sentCount += _batchSize;
                logger.LogDebug("Sent batch. Total sent: {SentCount}/{Total}", sentCount, _payloadLimit);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error sending batch. Retrying...");
            }

            await Task.Delay(TimeSpan.FromSeconds(_waitTimeSeconds), stoppingToken);
        }

        logger.LogInformation("Generator finished. Total rentals sent: {SentCount}", sentCount);
    }

    private void ValidateConfiguration()
    {
        if (_batchSize <= 0)
            throw new ArgumentException($"Invalid BatchSize: {_batchSize}", nameof(_batchSize));

        if (_payloadLimit <= 0)
            throw new ArgumentException($"Invalid PayloadLimit: {_payloadLimit}", nameof(_payloadLimit));

        if (_waitTimeSeconds <= 0)
            throw new ArgumentException($"Invalid WaitTime: {_waitTimeSeconds}", nameof(_waitTimeSeconds));
    }
}