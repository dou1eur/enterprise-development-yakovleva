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
/// PostgreSQL repository implementation for renter entities
/// Handles data access for renters with mapping between domain and entity models
/// </summary>
public class RenterRepository : CarRentalServiceBaseRepository<Renter, Guid>, IRenterRepository
{
    private new readonly CarRentalDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of RenterRepository
    /// </summary>
    /// <param name="dbContext">The database context</param>
    public RenterRepository(CarRentalDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves a renter by their driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter if found, otherwise null</returns>
    public async Task<Renter?> GetByLicenseNumberAsync(string licenseNumber)
    {
        var entity = await _dbContext.Renters
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.LicenseNumber == licenseNumber);

        return entity?.ToDomain();
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public override async Task<Renter?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Renters
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);

        return entity?.ToDomain();
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public override async Task<List<Renter>> GetAllAsync()
    {
        var entities = await _dbContext.Renters
            .AsNoTracking()
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public override async Task<Renter> AddAsync(Renter entity)
    {
        var dbEntity = entity.ToEntity();
        var result = await _dbContext.Renters.AddAsync(dbEntity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.ToDomain();
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public override async Task<Renter> UpdateAsync(Renter entity)
    {
        var dbEntity = entity.ToEntity();
        var result = _dbContext.Renters.Update(dbEntity);
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
        var entity = await _dbContext.Renters.FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.Renters.Remove(entity);
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
        return await _dbContext.Renters.AnyAsync(r => r.Id == id);
    }
}