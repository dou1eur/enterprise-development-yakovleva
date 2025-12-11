using CarRentalService.Domain;

namespace CarRentalService.Interfaces.Repositories;

/// <summary>
/// Repository interface for rental entities with specific rental operations
/// </summary>
public interface IRentalRepository : IRepository<Rental, Guid>
{
    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The renter identifier</param>
    /// <returns>List of rentals for the specified renter</returns>
    public Task<List<Rental>> GetByRenterIdAsync(Guid renterId);

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier</param>
    /// <returns>List of rentals for the specified vehicle</returns>
    public Task<List<Rental>> GetByVehicleIdAsync(Guid vehicleId);
}