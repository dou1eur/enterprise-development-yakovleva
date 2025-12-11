using CarRentalService.Domain;

namespace CarRentalService.Interfaces.Repositories;

/// <summary>
/// Repository interface for vehicle entities with specific vehicle operations
/// </summary>
public interface IVehicleRepository : IRepository<Vehicle, Guid>
{
    /// <summary>
    /// Retrieves a vehicle by its license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle if found, otherwise null</returns>
    public Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of vehicles for the specified model</returns>
    public Task<List<Vehicle>> GetByModelAsync(Guid modelId);
}