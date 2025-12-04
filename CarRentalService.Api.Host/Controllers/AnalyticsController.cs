using CarRentalService.Application.Contracts.Common;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for analytical data and reports
/// Provides various analytical queries for business intelligence
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    /// <summary>
    /// Initializes a new instance of the AnalyticsController class
    /// </summary>
    /// <param name="analyticsService">The analytics service</param>
    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Gets all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    /// <param name="vehicleModelId">The vehicle model ID</param>
    /// <returns>List of renters with total spent amounts</returns>
    [HttpGet("renters-by-model/{vehicleModelId}")]
    public async Task<ActionResult<List<RenterTotalSpentResponse>>> GetRentersByVehicleModel(Guid vehicleModelId)
    {
        var result = await _analyticsService.GetRentersByVehicleModelAsync(vehicleModelId);
        return Ok(result);
    }

    /// <summary>
    /// Gets all vehicles that are currently rented
    /// </summary>
    /// <returns>List of currently rented vehicles with rental counts</returns>
    [HttpGet("vehicles-currently-rented")]
    public async Task<ActionResult<List<VehicleRentalCountResponse>>> GetVehiclesCurrentlyRented()
    {
        var result = await _analyticsService.GetVehiclesCurrentlyRentedAsync();
        return Ok(result);
    }

    /// <summary>
    /// Gets top N most frequently rented vehicles
    /// </summary>
    /// <param name="top">Number of top vehicles to return (default: 5)</param>
    /// <returns>List of top rented vehicles with rental counts</returns>
    [HttpGet("top-rented-vehicles")]
    public async Task<ActionResult<List<VehicleRentalCountResponse>>> GetTopRentedVehicles([FromQuery] int top = 5)
    {
        var result = await _analyticsService.GetTopRentedVehiclesAsync(top);
        return Ok(result);
    }

    /// <summary>
    /// Gets the number of rentals for each vehicle
    /// </summary>
    /// <returns>List of vehicles with their rental counts</returns>
    [HttpGet("rental-count-per-vehicle")]
    public async Task<ActionResult<List<VehicleRentalCountResponse>>> GetRentalCountPerVehicle()
    {
        var result = await _analyticsService.GetRentalCountPerVehicleAsync();
        return Ok(result);
    }

    /// <summary>
    /// Gets top N renters by total amount spent on rentals
    /// </summary>
    /// <param name="top">Number of top renters to return (default: 5)</param>
    /// <returns>List of top renters with total spent amounts</returns>
    [HttpGet("top-renters-by-spent")]
    public async Task<ActionResult<List<RenterTotalSpentResponse>>> GetTopRentersByRentalSum([FromQuery] int top = 5)
    {
        var result = await _analyticsService.GetTopRentersByRentalSumAsync(top);
        return Ok(result);
    }
}