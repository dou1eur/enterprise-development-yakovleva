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
/// PostgreSQL repository implementation for vehicle model entities
/// Handles data access for vehicle models with mapping between domain and entity models
/// </summary>
public class VehicleModelRepository : CarRentalServiceBaseRepository<VehicleModel, Guid>, IVehicleModelRepository
{
    private new readonly CarRentalDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of VehicleModelRepository
    /// </summary>
    /// <param name="dbContext">The database context</param>
    public VehicleModelRepository(CarRentalDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model if found, otherwise null</returns>
    public async Task<VehicleModel?> GetByNameAsync(string name)
    {
        var entity = await _dbContext.VehicleModels
            .AsNoTracking()
            .FirstOrDefaultAsync(vm => vm.Name == name);

        return entity?.ToDomain();
    }

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle models with the specified body type</returns>
    public async Task<List<VehicleModel>> GetByBodyTypeAsync(BodyType bodyType)
    {
        var entities = await _dbContext.VehicleModels
            .AsNoTracking()
            .Where(vm => vm.BodyType == bodyType)
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle models with the specified vehicle class</returns>
    public async Task<List<VehicleModel>> GetByVehicleClassAsync(VehicleClass vehicleClass)
    {
        var entities = await _dbContext.VehicleModels
            .AsNoTracking()
            .Where(vm => vm.VehicleClass == vehicleClass)
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public override async Task<VehicleModel?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.VehicleModels
            .AsNoTracking()
            .FirstOrDefaultAsync(vm => vm.Id == id);

        return entity?.ToDomain();
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public override async Task<List<VehicleModel>> GetAllAsync()
    {
        var entities = await _dbContext.VehicleModels
            .AsNoTracking()
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public override async Task<VehicleModel> AddAsync(VehicleModel entity)
    {
        var dbEntity = entity.ToEntity();
        var result = await _dbContext.VehicleModels.AddAsync(dbEntity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.ToDomain();
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public override async Task<VehicleModel> UpdateAsync(VehicleModel entity)
    {
        var dbEntity = entity.ToEntity();
        var result = _dbContext.VehicleModels.Update(dbEntity);
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
        var entity = await _dbContext.VehicleModels.FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.VehicleModels.Remove(entity);
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
        return await _dbContext.VehicleModels.AnyAsync(vm => vm.Id == id);
    }
}