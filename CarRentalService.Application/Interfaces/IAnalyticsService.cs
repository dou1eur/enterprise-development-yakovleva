using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service for providing analytical data and reports
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Returns all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    /// <param name="vehicleModelId">The vehicle model identifier</param>
    /// <returns>List of renters ordered by full name</returns>
    public Task<List<Renter>> GetRentersByVehicleModelAsync(Guid vehicleModelId);

    /// <summary>
    /// Returns all vehicles that are currently rented
    /// </summary>
    /// <returns>List of currently rented vehicles</returns>
    public Task<List<Vehicle>> GetVehiclesCurrentlyRentedAsync();

    /// <summary>
    /// Returns top N most frequently rented vehicles
    /// </summary>
    /// <param name="top">Number of top vehicles to return</param>
    /// <returns>List of vehicles with rental counts</returns>
    public Task<List<(Vehicle Vehicle, int RentalCount)>> GetTopRentedVehiclesAsync(int top = 5);

    /// <summary>
    /// Returns the number of rentals for each vehicle
    /// </summary>
    /// <returns>List of vehicles with their rental counts</returns>
    public Task<List<(Vehicle Vehicle, int RentalCount)>> GetRentalCountPerVehicleAsync();

    /// <summary>
    /// Returns top N renters by total amount spent on rentals
    /// </summary>
    /// <param name="top">Number of top renters to return</param>
    /// <returns>List of renters with total spent amounts</returns>
    public Task<List<(Renter Renter, decimal TotalSpent)>> GetTopRentersByRentalSumAsync(int top = 5);
}
