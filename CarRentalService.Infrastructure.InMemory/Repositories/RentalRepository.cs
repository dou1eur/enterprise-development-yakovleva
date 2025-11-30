using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for rental entities
/// </summary>
public class RentalRepository : BaseRepository<Rental, Guid>, IRentalRepository
{
    /// <summary>
    /// Extracts the identifier from a rental entity
    /// </summary>
    /// <param name="entity">The rental entity</param>
    /// <returns>The rental identifier</returns>
    protected override object? GetId(Rental entity) => entity.Id;

    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The renter identifier</param>
    /// <returns>List of rentals for the specified renter</returns>
    public Task<List<Rental>> GetByRenterIdAsync(Guid renterId)
    {
        var rentals = _entities.Where(r => r.RenterId == renterId).ToList();
        return Task.FromResult(rentals);
    }

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier</param>
    /// <returns>List of rentals for the specified vehicle</returns>
    public Task<List<Rental>> GetByVehicleIdAsync(Guid vehicleId)
    {
        var rentals = _entities.Where(r => r.VehicleId == vehicleId).ToList();
        return Task.FromResult(rentals);
    }
}