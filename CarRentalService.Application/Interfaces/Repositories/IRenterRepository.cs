using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces.Repositories;

public interface IRenterRepository : IRepository<Renter, Guid>
{
    public Task<Renter?> GetByLicenseNumberAsync(string licenseNumber);
}