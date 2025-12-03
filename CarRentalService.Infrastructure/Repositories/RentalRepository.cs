using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Infrastructure.Entities;
using CarRentalService.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for rental entities
/// Handles data access for rentals with mapping between domain and entity models
/// Includes cost calculation based on vehicle's model generation rental price
/// </summary>
public class RentalRepository : CarRentalServiceBaseRepository<Rental, Guid>, IRentalRepository
{
    private new readonly CarRentalDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of RentalRepository
    /// </summary>
    /// <param name="dbContext">The database context</param>
    public RentalRepository(CarRentalDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

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

        return entities.Select(e => e.ToDomain()).ToList();
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

        return entities.Select(e => e.ToDomain()).ToList();
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

        return entity?.ToDomain();
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

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public override async Task<Rental> AddAsync(Rental entity)
    {
        var dbEntity = entity.ToEntity();
        var result = await _dbContext.Rentals.AddAsync(dbEntity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.ToDomain();
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public override async Task<Rental> UpdateAsync(Rental entity)
    {
        var dbEntity = entity.ToEntity();
        var result = _dbContext.Rentals.Update(dbEntity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.ToDomain();
    }

    /// <summary>
    /// Deletes an entity by its identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public override async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Rentals.FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.Rentals.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
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
