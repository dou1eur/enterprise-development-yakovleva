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
/// PostgreSQL repository implementation for model generation entities
/// Handles data access for model generations with mapping between domain and entity models
/// </summary>
public class ModelGenerationRepository : CarRentalServiceBaseRepository<ModelGeneration, Guid>, IModelGenerationRepository
{
    private new readonly CarRentalDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of ModelGenerationRepository
    /// </summary>
    /// <param name="dbContext">The database context</param>
    public ModelGenerationRepository(CarRentalDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of generations for the specified model</returns>
    public async Task<List<ModelGeneration>> GetByModelIdAsync(Guid modelId)
    {
        var entities = await _dbContext.ModelGenerations
            .AsNoTracking()
            .Where(mg => mg.VehicleModelId == modelId)
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generations within the specified year range</returns>
    public async Task<List<ModelGeneration>> GetByYearRangeAsync(int startYear, int endYear)
    {
        var entities = await _dbContext.ModelGenerations
            .AsNoTracking()
            .Where(mg => mg.Year >= startYear && mg.Year <= endYear)
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public override async Task<ModelGeneration?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.ModelGenerations
            .AsNoTracking()
            .FirstOrDefaultAsync(mg => mg.Id == id);

        return entity?.ToDomain();
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public override async Task<List<ModelGeneration>> GetAllAsync()
    {
        var entities = await _dbContext.ModelGenerations
            .AsNoTracking()
            .ToListAsync();

        return entities.Select(e => e.ToDomain()).ToList();
    }

    /// <summary>
    /// Adds a new entity to the repository
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The added entity</returns>
    public override async Task<ModelGeneration> AddAsync(ModelGeneration entity)
    {
        var dbEntity = entity.ToEntity();
        var result = await _dbContext.ModelGenerations.AddAsync(dbEntity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.ToDomain();
    }

    /// <summary>
    /// Updates an existing entity in the repository
    /// </summary>
    /// <param name="entity">The entity with updated data</param>
    /// <returns>The updated entity</returns>
    public override async Task<ModelGeneration> UpdateAsync(ModelGeneration entity)
    {
        var dbEntity = entity.ToEntity();
        var result = _dbContext.ModelGenerations.Update(dbEntity);
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
        var entity = await _dbContext.ModelGenerations.FindAsync(id);
        if (entity == null)
            return false;

        _dbContext.ModelGenerations.Remove(entity);
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
        return await _dbContext.ModelGenerations.AnyAsync(mg => mg.Id == id);
    }
}