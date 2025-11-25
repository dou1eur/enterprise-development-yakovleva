using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for renter entities with specific renter operations
/// </summary>
public interface IRenterRepository : IRepository<Renter, Guid>
{
    /// <summary>
    /// Retrieves a renter by their driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter if found, otherwise null</returns>
    public Task<Renter?> GetByLicenseNumberAsync(string licenseNumber);
}