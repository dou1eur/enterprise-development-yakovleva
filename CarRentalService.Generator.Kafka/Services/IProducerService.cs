using CarRentalService.Application.Contracts.Rental;

namespace CarRentalService.Generator.Kafka.Services;

/// <summary>
/// Interface for sending messages to Kafka
/// </summary>
public interface IProducerService
{
    /// <summary>
    /// Sends a list of rental contracts to Kafka
    /// </summary>
    /// <param name="rentals">List of rental contracts to send</param>
    /// <returns>Task representing the asynchronous operation</returns>
    public Task SendAsync(IList<RentalRequest> rentals);
}