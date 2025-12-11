using AutoMapper;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for vehicle entities
/// Handles data access for vehicles with mapping between domain and entity models
/// </summary>
public class VehicleRepository(
    CarRentalDbContext dbContext,
    IMapper mapper)
    : CarRentalServiceBaseRepository<VehicleEntity, Vehicle, Guid>(dbContext, mapper),
      IVehicleRepository
{
    /// <summary>
    /// Retrieves a vehicle by its license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle if found, otherwise null</returns>
    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate)
    {
        var entity = await _dbContext.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);

        return entity == null ? null : _mapper.Map<Vehicle>(entity);
    }

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of vehicles for the specified model</returns>
    public async Task<List<Vehicle>> GetByModelAsync(Guid modelId)
    {
        var entities = await _dbContext.Vehicles
            .AsNoTracking()
            .Include(v => v.ModelGeneration)
            .Where(v => v.ModelGeneration.VehicleModelId == modelId)
            .ToListAsync();

        return _mapper.Map<List<Vehicle>>(entities);
    }

    /// <summary>
    /// Retrieves an entity by its unique identifier
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>The entity if found, otherwise null</returns>
    public override async Task<Vehicle?> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);

        return entity == null ? null : _mapper.Map<Vehicle>(entity);
    }

    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public override async Task<List<Vehicle>> GetAllAsync()
    {
        var entities = await _dbContext.Vehicles
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Vehicle>>(entities);
    }

    /// <summary>
    /// Checks if an entity with the specified identifier exists
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <returns>True if entity exists, otherwise false</returns>
    public override async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Vehicles.AnyAsync(v => v.Id == id);
    }
}