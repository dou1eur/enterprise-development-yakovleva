using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for vehicle generation entities
/// </summary>
public interface IVehicleGenerationRepository : IRepository<VehicleGeneration, Guid>
{
    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of generations for the specified model</returns>
    public Task<List<VehicleGeneration>> GetByModelIdAsync(Guid modelId);

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generations within the specified year range</returns>
    public Task<List<VehicleGeneration>> GetByYearRangeAsync(int startYear, int endYear);
}