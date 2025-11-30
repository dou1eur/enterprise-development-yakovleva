using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller providing analytics endpoints for rentals, vehicles, and renters
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly ILogger<AnalyticsController> _logger;

    /// <summary>
    /// Initializes a new instance of AnalyticsController
    /// </summary>
    /// <param name="analyticsService">Analytics service handling complex queries</param>
    /// <param name="logger">Logger instance</param>
    public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    /// <summary>
    /// Returns all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    /// <param name="vehicleModelId">The vehicle model identifier</param>
    /// <returns>List of renters ordered by full name</returns>
    [HttpGet("renters-by-model/{vehicleModelId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RenterCollectionResponse>> GetRentersByVehicleModel(Guid vehicleModelId)
    {
        _logger.LogInformation("Called GetRentersByVehicleModel with model ID: {VehicleModelId}", vehicleModelId);

        var result = await _analyticsService.GetRentersByVehicleModelAsync(vehicleModelId);
        if (result.Count == 0)
            return NotFound($"No renters found for vehicle model with ID {vehicleModelId}");

        var response = new RenterCollectionResponse(result.ConvertAll(r => r.ToDto()));
        return Ok(response);
    }

    /// <summary>
    /// Returns all vehicles that are currently rented
    /// </summary>
    /// <returns>List of currently rented vehicles</returns>
    [HttpGet("vehicles-rented")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VehicleCollectionResponse>> GetVehiclesCurrentlyRented()
    {
        _logger.LogInformation("Called GetVehiclesCurrentlyRented");

        var result = await _analyticsService.GetVehiclesCurrentlyRentedAsync();
        if (result.Count == 0)
            return NotFound("No vehicles are currently rented");

        var response = new VehicleCollectionResponse(result.ConvertAll(v => v.ToDto()));
        return Ok(response);
    }

    /// <summary>
    /// Returns top 5 most frequently rented vehicles
    /// </summary>
    /// <returns>List of top 5 most frequently rented vehicles with rental counts</returns>
    [HttpGet("top-vehicles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<VehicleRentalCountCollectionResponse>> GetTopVehicles()
    {
        _logger.LogInformation("Called GetTopVehicles");

        var result = await _analyticsService.GetTopRentedVehiclesAsync(5);
        var response = result
            .Select(x => new VehicleRentalCountDto(x.Vehicle.ToDto(), x.RentalCount))
            .ToList()
            .ToResponse();

        return Ok(response);
    }

    /// <summary>
    /// Returns the number of rentals for each vehicle
    /// </summary>
    /// <returns>List of vehicles with their rental counts</returns>
    [HttpGet("rental-count-per-vehicle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<VehicleRentalCountCollectionResponse>> GetRentalCountPerVehicle()
    {
        _logger.LogInformation("Called GetRentalCountPerVehicle");

        var result = await _analyticsService.GetRentalCountPerVehicleAsync();
        var response = result
            .Select(x => new VehicleRentalCountDto(x.Vehicle.ToDto(), x.RentalCount))
            .ToList()
            .ToResponse();

        return Ok(response);
    }

    /// <summary>
    /// Returns top 5 renters by total amount spent on rentals
    /// </summary>
    /// <returns>List of top 5 renters by total spent amounts</returns>
    [HttpGet("top-renters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RenterTotalSpentCollectionResponse>> GetTopRentersByRentalSum()
    {
        _logger.LogInformation("Called GetTopRentersByRentalSum");

        var result = await _analyticsService.GetTopRentersByRentalSumAsync(5);
        var response = result
            .Select(x => new RenterTotalSpentDto(x.Renter.ToDto(), x.TotalSpent))
            .ToList()
            .ToResponse();

        return Ok(response);
    }
}