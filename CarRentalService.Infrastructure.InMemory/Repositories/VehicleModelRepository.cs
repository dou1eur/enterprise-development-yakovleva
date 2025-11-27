using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for vehicle model entities
/// </summary>
public class VehicleModelRepository : BaseRepository<VehicleModel, Guid>, IVehicleModelRepository
{
    /// <summary>
    /// Extracts the identifier from a vehicle model entity
    /// </summary>
    /// <param name="entity">The vehicle model entity</param>
    /// <returns>The model identifier</returns>
    protected override object? GetId(VehicleModel entity) => entity.Id;

    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model if found, otherwise null</returns>
    public Task<VehicleModel?> GetByNameAsync(string name)
    {
        var model = _entities.FirstOrDefault(m =>
            m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(model);
    }

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle models with the specified body type</returns>
    public Task<List<VehicleModel>> GetByBodyTypeAsync(BodyType bodyType)
    {
        var models = _entities.Where(m => m.BodyType == bodyType).ToList();
        return Task.FromResult(models);
    }

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle models with the specified vehicle class</returns>
    public Task<List<VehicleModel>> GetByVehicleClassAsync(VehicleClass vehicleClass)
    {
        var models = _entities.Where(m => m.VehicleClass == vehicleClass).ToList();
        return Task.FromResult(models);
    }
}