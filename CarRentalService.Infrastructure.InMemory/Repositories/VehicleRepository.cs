using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for vehicle entities
/// </summary>
public class VehicleRepository : BaseRepository<Vehicle, Guid>, IVehicleRepository
{
    /// <summary>
    /// Extracts the identifier from a vehicle entity
    /// </summary>
    /// <param name="entity">The vehicle entity</param>
    /// <returns>The vehicle identifier</returns>
    protected override object? GetId(Vehicle entity) => entity.Id;

    /// <summary>
    /// Retrieves a vehicle by its license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle if found, otherwise null</returns>
    public Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
    {
        var vehicle = _entities.FirstOrDefault(v =>
            v.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(vehicle);
    }

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of vehicles for the specified model</returns>
    public Task<List<Vehicle>> GetByModelAsync(Guid modelId)
    {
        var vehicles = _entities.Where(v => v.Generation.Model.Id == modelId).ToList();
        return Task.FromResult(vehicles);
    }
}