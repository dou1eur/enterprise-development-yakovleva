using AutoMapper;
using CarRentalService.Application.Contracts.Common;
using CarRentalService.Application.Interfaces;
using CarRentalService.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for providing analytical data and reports for the car rental system
/// Handles complex queries and data aggregation for business intelligence
/// </summary>
/// <param name="rentalRepository">Repository for rentals</param>
/// <param name="vehicleRepository">Repository for vehicles</param>
/// <param name="renterRepository">Repository for renters</param>
/// <param name="modelGenerationRepository">Repository for model generations</param>
/// <param name="mapper">AutoMapper instance</param>
/// <param name="logger">Logger instance</param>
public class AnalyticsService(
    IRentalRepository rentalRepository,
    IVehicleRepository vehicleRepository,
    IRenterRepository renterRepository,
    IModelGenerationRepository modelGenerationRepository,
    IMapper mapper,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <summary>
    /// Returns all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    /// <param name="vehicleModelId">The vehicle model identifier</param>
    /// <returns>List of renters with total spent amounts</returns>
    public async Task<List<RenterTotalSpentResponse>> GetRentersByVehicleModelAsync(Guid vehicleModelId)
    {
        logger.LogInformation("Getting renters by vehicle model {VehicleModelId}", vehicleModelId);

        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();
        var renters = await renterRepository.GetAllAsync();
        var modelGenerations = await modelGenerationRepository.GetAllAsync();

        var result = rentals
            .Where(rental =>
            {
                var vehicle = vehicles.FirstOrDefault(v => v.Id == rental.VehicleId);
                if (vehicle is null) return false;

                var modelGeneration = modelGenerations.FirstOrDefault(mg => mg.Id == vehicle.GenerationId);
                return modelGeneration is not null && modelGeneration.VehicleModelId == vehicleModelId;
            })
            .Select(rental => renters.First(r => r.Id == rental.RenterId))
            .Distinct()
            .OrderBy(renter => renter.FullName)
            .Select(renter =>
            {
                var renterRentals = rentals.Where(r => r.RenterId == renter.Id);
                var totalSpent = renterRentals.Sum(r => r.TotalCost);
                return (renter, totalSpent);
            })
            .ToList();

        logger.LogDebug("Found {Count} renters for vehicle model {VehicleModelId}", result.Count, vehicleModelId);

        return mapper.Map<List<RenterTotalSpentResponse>>(result);
    }

    /// <summary>
    /// Returns all vehicles that are currently rented
    /// </summary>
    /// <returns>List of currently rented vehicles with rental counts</returns>
    public async Task<List<VehicleRentalCountResponse>> GetVehiclesCurrentlyRentedAsync()
    {
        logger.LogInformation("Getting currently rented vehicles");

        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();

        var now = DateTime.UtcNow;
        var rentedVehicleIds = rentals
            .Where(rental => rental.RentStartTime <= now && rental.RentStartTime.AddHours(rental.DurationHours) >= now)
            .Select(rental => rental.VehicleId)
            .Distinct()
            .ToList();

        var rentedVehicles = vehicles
            .Where(vehicle => rentedVehicleIds.Contains(vehicle.Id))
            .Select(vehicle => (vehicle, RentalCount: 1))
            .ToList();

        logger.LogDebug("Found {Count} currently rented vehicles", rentedVehicles.Count);

        return mapper.Map<List<VehicleRentalCountResponse>>(rentedVehicles);
    }

    /// <summary>
    /// Returns top N most frequently rented vehicles
    /// </summary>
    /// <param name="top">Number of top vehicles to return (default: 5)</param>
    /// <returns>List of top rented vehicles with rental counts</returns>
    public async Task<List<VehicleRentalCountResponse>> GetTopRentedVehiclesAsync(int top = 5)
    {
        logger.LogInformation("Getting top {Top} rented vehicles", top);

        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.VehicleId)
            .Select(group => (
                Vehicle: vehicles.FirstOrDefault(vehicle => vehicle.Id == group.Key),
                RentalCount: group.Count()
            ))
            .Where(x => x.Vehicle is not null)
            .OrderByDescending(x => x.RentalCount)
            .Take(top)
            .Select(x => (x.Vehicle!, x.RentalCount))
            .ToList();

        logger.LogDebug("Retrieved top {Count} rented vehicles", result.Count);

        return mapper.Map<List<VehicleRentalCountResponse>>(result);
    }

    /// <summary>
    /// Returns the number of rentals for each vehicle
    /// </summary>
    /// <returns>List of vehicles with their rental counts</returns>
    public async Task<List<VehicleRentalCountResponse>> GetRentalCountPerVehicleAsync()
    {
        logger.LogInformation("Getting rental count per vehicle");

        var rentals = await rentalRepository.GetAllAsync();
        var vehicles = await vehicleRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.VehicleId)
            .Select(group => (
                Vehicle: vehicles.FirstOrDefault(vehicle => vehicle.Id == group.Key),
                RentalCount: group.Count()
            ))
            .Where(x => x.Vehicle is not null)
            .Select(x => (x.Vehicle!, x.RentalCount))
            .ToList();

        logger.LogDebug("Calculated rental counts for {Count} vehicles", result.Count);

        return mapper.Map<List<VehicleRentalCountResponse>>(result);
    }

    /// <summary>
    /// Returns top N renters by total amount spent on rentals
    /// </summary>
    /// <param name="top">Number of top renters to return (default: 5)</param>
    /// <returns>List of top renters with total spent amounts</returns>
    public async Task<List<RenterTotalSpentResponse>> GetTopRentersByRentalSumAsync(int top = 5)
    {
        logger.LogInformation("Getting top {Top} renters by rental sum", top);

        var rentals = await rentalRepository.GetAllAsync();
        var renters = await renterRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.RenterId)
            .Select(group =>
            {
                var total = group.Sum(rental => rental.TotalCost);
                var renter = renters.FirstOrDefault(r => r.Id == group.Key);
                return (renter, total);
            })
            .Where(x => x.renter is not null)
            .OrderByDescending(x => x.total)
            .Take(top)
            .Select(x => (x.renter!, x.total))
            .ToList();

        logger.LogDebug("Retrieved top {Count} renters by rental sum", result.Count);

        return mapper.Map<List<RenterTotalSpentResponse>>(result);
    }
}