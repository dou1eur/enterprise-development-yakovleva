using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for vehicle entities
/// Handles data access for vehicles with included model generation
/// </summary>
/// <param name="dbContext">The database context</param>
public class VehicleRepository(CarRentalDbContext dbContext)
    : BaseRepository<Vehicle, Guid>(dbContext), IVehicleRepository
{
    protected override IQueryable<Vehicle> GetBaseQuery() =>
        base.GetBaseQuery().Include(v => v.ModelGeneration);

    /// <summary>
    /// Retrieves a vehicle by its license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle if found, otherwise null</returns>
    public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate) =>
        await GetBaseQueryAsNoTracking()
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The vehicle model identifier</param>
    /// <returns>List of vehicles for the specified model</returns>
    public async Task<List<Vehicle>> GetByModelAsync(Guid modelId) =>
        await GetBaseQueryAsNoTracking()
            .Where(v => v.ModelGeneration!.VehicleModelId == modelId)
            .ToListAsync();
}