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

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public override async Task<Rental?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Rentals
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        return entity == null ? null : _mapper.Map<Rental>(entity);
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public override async Task<List<Rental>> GetAllAsync()
    {
        var entities = await _dbContext.Rentals
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Rental>>(entities);
    }

    /// <summary>
    /// Checks if an entity with the specified identifier exists
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if entity exists, otherwise false</returns>
    public override async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Rentals.AnyAsync(r => r.Id == id);
    }
}