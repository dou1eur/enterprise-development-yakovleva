using CarRentalService.Application.Contracts.Rental;
using Confluent.Kafka;

namespace CarRentalService.Generator.Kafka.Services;

/// <summary>
/// Service for sending messages to Kafka
/// </summary>
/// <param name="producer">Kafka producer instance</param>
/// <param name="configuration">Application configuration</param>
/// <param name="logger">Logger instance</param>
public class GeneratorService(
    IProducer<Guid, IList<RentalRequest>> producer,
    IConfiguration configuration,
    ILogger<GeneratorService> logger) : IProducerService
{
    private readonly string _topic = configuration["Kafka:Topic"]
        ?? throw new InvalidOperationException("Kafka topic not configured");

    /// <summary>
    /// Sends a list of rental contracts to Kafka
    /// </summary>
    public async Task SendAsync(IList<RentalRequest> rentals)
    {
        if (rentals is not { Count: > 0 })
        {
            logger.LogWarning("Attempted to send empty rentals list");
            return;
        }

        try
        {
            var message = new Message<Guid, IList<RentalRequest>>
            {
                Key = Guid.NewGuid(),
                Value = rentals,
                Timestamp = new Timestamp(DateTime.UtcNow)
            };

            var result = await producer.ProduceAsync(_topic, message);

            logger.LogInformation(
                "Sent {Count} rentals to {Topic} [Partition {Partition}, Offset {Offset}]",
                rentals.Count, result.Topic, result.Partition, result.Offset);
        }
        catch (ProduceException<Guid, IList<RentalRequest>> ex)
        {
            logger.LogError(ex, "Failed to send rentals to Kafka");
            throw;
        }
    }
}