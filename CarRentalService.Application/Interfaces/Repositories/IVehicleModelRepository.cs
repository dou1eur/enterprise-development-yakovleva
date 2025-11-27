using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for vehicle model entities
/// </summary>
public interface IVehicleModelRepository : IRepository<VehicleModel, Guid>
{
    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model if found, otherwise null</returns>
    public Task<VehicleModel?> GetByNameAsync(string name);

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle models with the specified body type</returns>
    public Task<List<VehicleModel>> GetByBodyTypeAsync(BodyType bodyType);

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle models with the specified vehicle class</returns>
    public Task<List<VehicleModel>> GetByVehicleClassAsync(VehicleClass vehicleClass);
}