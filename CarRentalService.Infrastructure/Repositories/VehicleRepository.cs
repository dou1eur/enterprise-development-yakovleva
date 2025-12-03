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
/// PostgreSQL repository implementation for vehicle entities
/// Handles data access for vehicles with mapping between domain and entity models
/// </summary>
public class VehicleRepository : CarRentalServiceBaseRepository<Vehicle, Guid>, IVehicleRepository
{
    private new readonly CarRentalDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of VehicleRepository
    /// </summary>
    /// <param name="dbContext">The database context</param>
    public VehicleRepository(CarRentalDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves a vehicle by its license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle if found, otherwise null</returns>
    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
    {
        var entity = await _dbContext.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);

        return entity?.ToDomain();
    }

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of vehicles for the specified model</returns>
    public async Task<List<Vehicle>> GetByModelAsync(Guid modelId)
    {
        var entities = await _dbContext.Vehicles
            .AsNoTracking()
            .Where(v => v.ModelGeneration.VehicleModelId == modelId)
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public override async Task<Vehicle?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);

        return entity?.ToDomain();
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public override async Task<List<Vehicle>> GetAllAsync()
    {
        var entities = await _dbContext.Vehicles
            .AsNoTracking()
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public override async Task<Vehicle> AddAsync(Vehicle entity)
    {
        var dbEntity = entity.ToEntity();
        var result = await _dbContext.Vehicles.AddAsync(dbEntity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.ToDomain();
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public override async Task<Vehicle> UpdateAsync(Vehicle entity)
    {
        var dbEntity = entity.ToEntity();
        var result = _dbContext.Vehicles.Update(dbEntity);
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
        var entity = await _dbContext.Vehicles.FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.Vehicles.Remove(entity);
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
        return await _dbContext.Vehicles.AnyAsync(v => v.Id == id);
    }
}
