using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for providing analytical data and reports for the car rental system
/// Handles complex queries and data aggregation for business intelligence
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRenterRepository _renterRepository;
    private readonly IModelGenerationRepository _modelGenerationRepository;
    private readonly IVehicleModelRepository _vehicleModelRepository;

    /// <summary>
    /// Initializes a new instance of AnalyticsService
    /// </summary>
    /// <param name="rentalRepository">The rental repository for rental data access</param>
    /// <param name="vehicleRepository">The vehicle repository for vehicle data access</param>
    /// <param name="renterRepository">The renter repository for renter data access</param>
    /// <param name="modelGenerationRepository">The model generation repository for generation data access</param>
    /// <param name="vehicleModelRepository">The vehicle model repository for model data access</param>
    public AnalyticsService(
        IRentalRepository rentalRepository,
        IVehicleRepository vehicleRepository,
        IRenterRepository renterRepository,
        IModelGenerationRepository modelGenerationRepository,
        IVehicleModelRepository vehicleModelRepository)
    {
        _rentalRepository = rentalRepository;
        _vehicleRepository = vehicleRepository;
        _renterRepository = renterRepository;
        _modelGenerationRepository = modelGenerationRepository;
        _vehicleModelRepository = vehicleModelRepository;
    }

    /// <summary>
    /// Returns all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    /// <param name="vehicleModelId">The vehicle model identifier</param>
    /// <returns>List of renters ordered by full name</returns>
    public async Task<List<Renter>> GetRentersByVehicleModelAsync(Guid vehicleModelId)
    {
        var rentals = await _rentalRepository.GetAllAsync();
        var vehicles = await _vehicleRepository.GetAllAsync();
        var renters = await _renterRepository.GetAllAsync();
        var modelGenerations = await _modelGenerationRepository.GetAllAsync();

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

        return result;
    }

    /// <summary>
    /// Returns all vehicles that are currently rented
    /// </summary>
    /// <returns>List of currently rented vehicles</returns>
    public async Task<List<Vehicle>> GetVehiclesCurrentlyRentedAsync()
    {
        var rentals = await _rentalRepository.GetAllAsync();
        var vehicles = await _vehicleRepository.GetAllAsync();

        var now = DateTime.Now;
        var rentedVehicleIds = rentals
            .Where(rental => rental.RentStartTime <= now &&
                            rental.RentStartTime.AddHours(rental.DurationHours) >= now)
            .Select(rental => rental.VehicleId)
            .Distinct();

        return vehicles.Where(vehicle => rentedVehicleIds.Contains(vehicle.Id)).ToList();
    }

    /// <summary>
    /// Returns top N most frequently rented vehicles
    /// </summary>
    /// <param name="top">Number of top vehicles to return</param>
    /// <returns>List of vehicles with rental counts</returns>
    public async Task<List<(Vehicle Vehicle, int RentalCount)>> GetTopRentedVehiclesAsync(int top = 5)
    {
        var rentals = await _rentalRepository.GetAllAsync();
        var vehicles = await _vehicleRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.VehicleId)
            .Select(group => (
                Vehicle: vehicles.First(vehicle => vehicle.Id == group.Key),
                RentalCount: group.Count()
            ))
            .OrderByDescending(x => x.RentalCount)
            .Take(top)
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns the number of rentals for each vehicle
    /// </summary>
    /// <returns>List of vehicles with their rental counts</returns>
    public async Task<List<(Vehicle Vehicle, int RentalCount)>> GetRentalCountPerVehicleAsync()
    {
        var rentals = await _rentalRepository.GetAllAsync();
        var vehicles = await _vehicleRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.VehicleId)
            .Select(group => (
                Vehicle: vehicles.First(vehicle => vehicle.Id == group.Key),
                RentalCount: group.Count()
            ))
            .ToList();

        return result;
    }

    /// <summary>
    /// Returns top N renters by total amount spent on rentals
    /// </summary>
    /// <param name="top">Number of top renters to return</param>
    /// <returns>List of renters with total spent amounts</returns>
    public async Task<List<(Renter Renter, decimal TotalSpent)>> GetTopRentersByRentalSumAsync(int top = 5)
    {
        var rentals = await _rentalRepository.GetAllAsync();
        var renters = await _renterRepository.GetAllAsync();
        var vehicles = await _vehicleRepository.GetAllAsync();
        var modelGenerations = await _modelGenerationRepository.GetAllAsync();

        var result = rentals
            .GroupBy(rental => rental.RenterId)
            .Select(group =>
            {
                var total = group.Sum(rental => rental.TotalCost);
                return (
                    Renter: renters.First(renter => renter.Id == group.Key),
                    TotalSpent: total
                );
            })
            .OrderByDescending(x => x.TotalSpent)
            .Take(top)
            .ToList();

        return result;
    }
}