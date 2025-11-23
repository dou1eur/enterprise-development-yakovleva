using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces.Repositories;

public interface IVehicleRepository : IRepository<Vehicle, Guid>
{
    public Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);
    public Task<List<Vehicle>> GetByModelAsync(Guid modelId);
}