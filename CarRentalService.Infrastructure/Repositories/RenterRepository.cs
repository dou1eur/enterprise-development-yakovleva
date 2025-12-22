using CarRentalService.Domain;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Repositories;

/// <summary>
/// PostgreSQL repository implementation for renter entities
/// Handles data access for renters
/// </summary>
/// <param name="dbContext">The database context</param>
public class RenterRepository(CarRentalDbContext dbContext)
    : BaseRepository<Renter, Guid>(dbContext), IRenterRepository
{
    /// <summary>
    /// Retrieves a renter by their driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter if found, otherwise null</returns>
    public async Task<Renter?> GetByLicenseNumberAsync(string licenseNumber) =>
        await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.LicenseNumber == licenseNumber);
}