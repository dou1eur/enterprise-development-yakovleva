using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces.Repositories;

public interface IRentalRepository : IRepository<Rental, Guid>
{
    public Task<List<Rental>> GetByRenterIdAsync(Guid renterId);
    public Task<List<Rental>> GetByVehicleIdAsync(Guid vehicleId);
}