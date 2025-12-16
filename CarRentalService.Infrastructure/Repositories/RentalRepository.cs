using AutoMapper;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for rental entities
/// Handles data access for rentals with mapping between domain and entity models
/// Includes cost calculation based on vehicle's model generation rental price
/// </summary>
public class RentalRepository(
    CarRentalDbContext dbContext,
    IMapper mapper)
    : CarRentalServiceBaseRepository<RentalEntity, Rental, Guid>(dbContext, mapper),
      IRentalRepository
{
    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The renter identifier</param>
    /// <returns>List of rentals for the specified renter</returns>
    public async Task<List<Rental>> GetByRenterIdAsync(Guid renterId)
    {
        var entities = await _dbContext.Rentals
            .AsNoTracking()
            .Where(r => r.RenterId == renterId)
            .ToListAsync();

        return _mapper.Map<List<Rental>>(entities);
    }

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier</param>
    /// <returns>List of rentals for the specified vehicle</returns>
    public async Task<List<Rental>> GetByVehicleIdAsync(Guid vehicleId)
    {
        var entities = await _dbContext.Rentals
            .AsNoTracking()
            .Where(r => r.VehicleId == vehicleId)
            .ToListAsync();

        return _mapper.Map<List<Rental>>(entities);
    }
}