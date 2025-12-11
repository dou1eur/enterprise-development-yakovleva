using AutoMapper;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for model generation entities
/// Handles data access for model generations with mapping between domain and entity models
/// </summary>
public class ModelGenerationRepository(
    CarRentalDbContext dbContext,
    IMapper mapper)
    : CarRentalServiceBaseRepository<ModelGenerationEntity, ModelGeneration, Guid>(dbContext, mapper),
      IModelGenerationRepository
{
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

        return _mapper.Map<List<ModelGeneration>>(entities);
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

        return _mapper.Map<List<ModelGeneration>>(entities);
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

        return entity == null ? null : _mapper.Map<ModelGeneration>(entity);
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

        return _mapper.Map<List<ModelGeneration>>(entities);
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