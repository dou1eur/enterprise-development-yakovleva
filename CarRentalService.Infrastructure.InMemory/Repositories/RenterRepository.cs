using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for renter entities
/// </summary>
public class RenterRepository : BaseRepository<Renter, Guid>, IRenterRepository
{
    /// <summary>
    /// Extracts the identifier from a renter entity
    /// </summary>
    /// <param name="entity">The renter entity</param>
    /// <returns>The renter identifier</returns>
    protected override object? GetId(Renter entity) => entity.Id;

    /// <summary>
    /// Retrieves a renter by their driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter if found, otherwise null</returns>
    public Task<Renter?> GetByLicenseNumberAsync(string licenseNumber)
    {
        var renter = _entities.FirstOrDefault(r =>
            r.LicenseNumber.Equals(licenseNumber, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(renter);
    }
}