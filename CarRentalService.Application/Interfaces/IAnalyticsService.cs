using CarRentalService.Application.Contracts.Common;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service for providing analytical data and reports
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Returns all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    public Task<List<RenterTotalSpentResponse>> GetRentersByVehicleModelAsync(Guid vehicleModelId);

    /// <summary>
    /// Returns all vehicles that are currently rented
    /// </summary>
    public Task<List<VehicleRentalCountResponse>> GetVehiclesCurrentlyRentedAsync();

    /// <summary>
    /// Returns top N most frequently rented vehicles
    /// </summary>
    public Task<List<VehicleRentalCountResponse>> GetTopRentedVehiclesAsync(int top = 5);

    /// <summary>
    /// Returns the number of rentals for each vehicle
    /// </summary>
    public Task<List<VehicleRentalCountResponse>> GetRentalCountPerVehicleAsync();

    /// <summary>
    /// Returns top N renters by total amount spent on rentals
    /// </summary>
    public Task<List<RenterTotalSpentResponse>> GetTopRentersByRentalSumAsync(int top = 5);
}