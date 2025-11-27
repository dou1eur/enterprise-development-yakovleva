using CarRentalService.Application.Contracts;
using System;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Rental service interface for performing CRUD operations
/// Provides CRUD operations for rental entities
/// </summary>
public interface IRentalService : IApplicationCRUDService<RentalDto, RentalCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The unique identifier of the renter</param>
    /// <returns>List of rental data transfer objects for the specified renter</returns>
    public System.Collections.Generic.List<RentalDto> GetByRenterId(Guid renterId);

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <returns>List of rental data transfer objects for the specified vehicle</returns>
    public System.Collections.Generic.List<RentalDto> GetByVehicleId(Guid vehicleId);

    /// <summary>
    /// Calculates the rental cost for a vehicle and duration
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <param name="durationHours">The rental duration in hours</param>
    /// <returns>The calculated rental cost</returns>
    public decimal CalculateRentalCost(Guid vehicleId, int durationHours);
}