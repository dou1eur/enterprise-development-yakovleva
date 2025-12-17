using CarRentalService.Interfaces.Repositories;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for vehicle model entities
/// Handles data access for vehicle models
/// </summary>
/// <param name="dbContext">The database context</param>
public class VehicleModelRepository(CarRentalDbContext dbContext)
    : BaseRepository<VehicleModel, Guid>(dbContext), IVehicleModelRepository
{
    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model if found, otherwise null</returns>
    public async Task<VehicleModel?> GetByNameAsync(string name) =>
        await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(vm => vm.Name == name);

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle models with the specified body type</returns>
    public async Task<List<VehicleModel>> GetByBodyTypeAsync(BodyType bodyType) =>
        await DbSet
            .AsNoTracking()
            .Where(vm => vm.BodyType == bodyType)
            .ToListAsync();

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle models with the specified vehicle class</returns>
    public async Task<List<VehicleModel>> GetByVehicleClassAsync(VehicleClass vehicleClass) =>
        await DbSet
            .AsNoTracking()
            .Where(vm => vm.VehicleClass == vehicleClass)
            .ToListAsync();
}