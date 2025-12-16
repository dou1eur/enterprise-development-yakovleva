using AutoMapper;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for vehicle model entities
/// Handles data access for vehicle models with mapping between domain and entity models
/// </summary>
public class VehicleModelRepository(
    CarRentalDbContext dbContext,
    IMapper mapper)
    : CarRentalServiceBaseRepository<VehicleModelEntity, VehicleModel, Guid>(dbContext, mapper),
      IVehicleModelRepository
{
    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model if found, otherwise null</returns>
    public async Task<VehicleModel?> GetByNameAsync(string name)
    {
        var entity = await _dbContext.VehicleModels
            .AsNoTracking()
            .FirstOrDefaultAsync(vm => vm.Name == name);

        return entity == null ? null : _mapper.Map<VehicleModel>(entity);
    }

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle models with the specified body type</returns>
    public async Task<List<VehicleModel>> GetByBodyTypeAsync(BodyType bodyType)
    {
        var entities = await _dbContext.VehicleModels
            .AsNoTracking()
            .Where(vm => vm.BodyType == bodyType)
            .ToListAsync();

        return _mapper.Map<List<VehicleModel>>(entities);
    }

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle models with the specified vehicle class</returns>
    public async Task<List<VehicleModel>> GetByVehicleClassAsync(VehicleClass vehicleClass)
    {
        var entities = await _dbContext.VehicleModels
            .AsNoTracking()
            .Where(vm => vm.VehicleClass == vehicleClass)
            .ToListAsync();

        return _mapper.Map<List<VehicleModel>>(entities);
    }
}