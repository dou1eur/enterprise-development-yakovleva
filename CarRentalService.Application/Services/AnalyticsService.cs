using CarRentalService.Application.Contracts.Common;
using CarRentalService.Application.Interfaces;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Application.Mappings;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for providing analytical data and reports for the car rental system
/// Handles complex queries and data aggregation for business intelligence
/// </summary>
public class AnalyticsService(
    IRentalRepository rentalRepository,
    IVehicleRepository vehicleRepository,
    IRenterRepository renterRepository,
    IModelGenerationRepository modelGenerationRepository)
    : IAnalyticsService
{
    /// <summary>
    /// Returns all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    public async Task<List<RenterTotalSpentResponse>> GetRentersByVehicleModelAsync(Guid vehicleModelId)
    {
        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();
        var renters = await renterRepository.GetAllAsync();
        var modelGenerations = await modelGenerationRepository.GetAllAsync();

        var result = rentals
            .Where(rental =>
            {
                var vehicle = vehicles.FirstOrDefault(v => v.Id == rental.VehicleId);
                if (vehicle == null) return false;
                var modelGeneration = modelGenerations.FirstOrDefault(mg => mg.Id == vehicle.GenerationId);
                return modelGeneration != null && modelGeneration.VehicleModelId == vehicleModelId;
            })
            .Select(rental => renters.First(r => r.Id == rental.RenterId))
            .Distinct()
            .OrderBy(renter => renter.FullName)
            .ToList();

        var renterTotalSpent = result.Select(renter =>
        {
            var renterRentals = rentals.Where(r => r.RenterId == renter.Id);
            var totalSpent = renterRentals.Sum(r => r.TotalCost);
            return (renter, totalSpent);
        }).ToList();

        return renterTotalSpent.ToResponseList();
    }

    /// <summary>
    /// Returns all vehicles that are currently rented
    /// </summary>
    public async Task<List<VehicleRentalCountResponse>> GetVehiclesCurrentlyRentedAsync()
    {
        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();

        var now = DateTime.Now;
        var rentedVehicleIds = rentals
            .Where(rental => rental.RentStartTime <= now &&
                   rental.RentStartTime.AddHours(rental.DurationHours) >= now)
            .Select(rental => rental.VehicleId)
            .Distinct();

        var rentedVehicles = vehicles
            .Where(vehicle => rentedVehicleIds.Contains(vehicle.Id))
            .Select(vehicle => (vehicle, 1)) 
            .ToList();

        return rentedVehicles.ToResponseList();
    }

    /// <summary>
    /// Returns top N most frequently rented vehicles
    /// </summary>
    public async Task<List<VehicleRentalCountResponse>> GetTopRentedVehiclesAsync(int top = 5)
    {
        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.VehicleId)
            .Select(group => (
                Vehicle: vehicles.First(vehicle => vehicle.Id == group.Key),
                RentalCount: group.Count()
            ))
            .OrderByDescending(x => x.RentalCount)
            .Take(top)
            .ToList();

        return result.ToResponseList();
    }

    /// <summary>
    /// Returns the number of rentals for each vehicle
    /// </summary>
    public async Task<List<VehicleRentalCountResponse>> GetRentalCountPerVehicleAsync()
    {
        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.VehicleId)
            .Select(group => (
                Vehicle: vehicles.First(vehicle => vehicle.Id == group.Key),
                RentalCount: group.Count()
            ))
            .ToList();

        return result.ToResponseList();
    }

    /// <summary>
    /// Returns top N renters by total amount spent on rentals
    /// </summary>
    public async Task<List<RenterTotalSpentResponse>> GetTopRentersByRentalSumAsync(int top = 5)
    {
        var rentals = await rentalRepository.GetAllAsync();
        var renters = await renterRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.RenterId)
            .Select(group =>
            {
                var total = group.Sum(rental => rental.TotalCost);
                var renter = renters.First(r => r.Id == group.Key);
                return (renter, total);
            })
            .OrderByDescending(x => x.total)
            .Take(top)
            .ToList();

        return result.ToResponseList();
    }
}