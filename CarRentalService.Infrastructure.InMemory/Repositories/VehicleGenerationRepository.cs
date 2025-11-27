using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for vehicle generation entities
/// </summary>
public class VehicleGenerationRepository : BaseRepository<VehicleGeneration, Guid>, IVehicleGenerationRepository
{
    /// <summary>
    /// Extracts the identifier from a vehicle generation entity
    /// </summary>
    /// <param name="entity">The vehicle generation entity</param>
    /// <returns>The generation identifier</returns>
    protected override object? GetId(VehicleGeneration entity) => entity.Id;

    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of generations for the specified model</returns>
    public Task<List<VehicleGeneration>> GetByModelIdAsync(Guid modelId)
    {
        var generations = _entities.Where(g => g.Model.Id == modelId).ToList();
        return Task.FromResult(generations);
    }

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generations within the specified year range</returns>
    public Task<List<VehicleGeneration>> GetByYearRangeAsync(int startYear, int endYear)
    {
        var generations = _entities.Where(g => g.Year >= startYear && g.Year <= endYear).ToList();
        return Task.FromResult(generations);
    }
}