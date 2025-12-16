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
}