using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for rental entities
/// Handles data access for rentals with included vehicle and renter
/// </summary>
/// <param name="dbContext">The database context</param>
public class RentalRepository(CarRentalDbContext dbContext)
    : BaseRepository<Rental, Guid>(dbContext), IRentalRepository
{
    protected override IQueryable<Rental> GetBaseQuery() =>
        base.GetBaseQuery()
            .Include(r => r.Vehicle!)
            .ThenInclude(v => v!.ModelGeneration)
            .Include(r => r.Renter!);

    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The renter identifier</param>
    /// <returns>List of rentals for the specified renter</returns>
    public async Task<List<Rental>> GetByRenterIdAsync(Guid renterId) =>
        await GetBaseQueryAsNoTracking()
            .Where(r => r.RenterId == renterId)
            .ToListAsync();

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The vehicle identifier</param>
    /// <returns>List of rentals for the specified vehicle</returns>
    public async Task<List<Rental>> GetByVehicleIdAsync(Guid vehicleId) =>
        await GetBaseQueryAsNoTracking()
            .Where(r => r.VehicleId == vehicleId)
            .ToListAsync();
}