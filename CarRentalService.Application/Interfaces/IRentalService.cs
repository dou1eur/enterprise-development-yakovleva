using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

public interface IRentalService
{
    public RentalDto Create(RentalCreateUpdateDto dto);
    public RentalDto Get(Guid id);
    public List<RentalDto> GetAll();
    public RentalDto Update(RentalCreateUpdateDto dto, Guid id);
    public bool Delete(Guid id);
    public List<RentalDto> GetByRenterId(Guid renterId);
    public List<RentalDto> GetByVehicleId(Guid vehicleId);
    public decimal CalculateRentalCost(Guid vehicleId, int durationHours);
}