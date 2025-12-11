using CarRentalService.Domain;

namespace CarRentalService.Interfaces.Repositories;

/// <summary>
/// Repository interface for vehicle generation entities
/// </summary>
public interface IModelGenerationRepository : IRepository<ModelGeneration, Guid>
{
    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of generations for the specified model</returns>
    public Task<List<ModelGeneration>> GetByModelIdAsync(Guid modelId);

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generations within the specified year range</returns>
    public Task<List<ModelGeneration>> GetByYearRangeAsync(int startYear, int endYear);
}