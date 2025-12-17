using CarRentalService.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for model generation entities
/// Handles data access for model generations with included vehicle model
/// </summary>
/// <param name="dbContext">The database context</param>
public class ModelGenerationRepository(CarRentalDbContext dbContext)
    : BaseRepository<ModelGeneration, Guid>(dbContext), IModelGenerationRepository
{
    protected override IQueryable<ModelGeneration> GetBaseQuery() =>
        base.GetBaseQuery().Include(mg => mg.VehicleModel);

    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of generations for the specified model</returns>
    public async Task<List<ModelGeneration>> GetByModelIdAsync(Guid modelId) =>
        await GetBaseQueryAsNoTracking()
            .Where(mg => mg.VehicleModelId == modelId)
            .ToListAsync();

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generations within the specified year range</returns>
    public async Task<List<ModelGeneration>> GetByYearRangeAsync(int startYear, int endYear) =>
        await GetBaseQueryAsNoTracking()
            .Where(mg => mg.Year >= startYear && mg.Year <= endYear)
            .ToListAsync();
}