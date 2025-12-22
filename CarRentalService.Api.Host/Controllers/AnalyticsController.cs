using CarRentalService.Application.Contracts.Common;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for analytical data and reports
/// Provides various analytical queries for business intelligence
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Gets all renters who rented vehicles of a specified model, ordered by full name
    /// </summary>
    /// <param name="vehicleModelId">The vehicle model ID</param>
    /// <returns>List of renters with total spent amounts</returns>
    [HttpGet("renters-by-model/{vehicleModelId}")]
    [ProducesResponseType(typeof(List<RenterTotalSpentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RenterTotalSpentResponse>>> GetRentersByVehicleModel(Guid vehicleModelId)
    {
        try
        {
            logger.LogInformation("Getting renters by vehicle model {VehicleModelId}", vehicleModelId);
            var result = await analyticsService.GetRentersByVehicleModelAsync(vehicleModelId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid request for vehicle model {VehicleModelId}", vehicleModelId);
            return BadRequest($"Invalid request data: {ex.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting renters by vehicle model {VehicleModelId}", vehicleModelId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets all vehicles that are currently rented
    /// </summary>
    /// <returns>List of currently rented vehicles with rental counts</returns>
    [HttpGet("vehicles-currently-rented")]
    [ProducesResponseType(typeof(List<VehicleRentalCountResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VehicleRentalCountResponse>>> GetVehiclesCurrentlyRented()
    {
        try
        {
            logger.LogInformation("Getting currently rented vehicles");
            var result = await analyticsService.GetVehiclesCurrentlyRentedAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting currently rented vehicles");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets top N most frequently rented vehicles
    /// </summary>
    /// <param name="top">Number of top vehicles to return (default: 5)</param>
    /// <returns>List of top rented vehicles with rental counts</returns>
    [HttpGet("top-rented-vehicles")]
    [ProducesResponseType(typeof(List<VehicleRentalCountResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VehicleRentalCountResponse>>> GetTopRentedVehicles([FromQuery, Range(1, 100)] int top = 5)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid top parameter: {Top}", top);
                return BadRequest("Invalid parameter value");
            }
            logger.LogInformation("Getting top {Top} rented vehicles", top);
            var result = await analyticsService.GetTopRentedVehiclesAsync(top);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid top parameter: {Top}", top);
            return BadRequest($"Invalid parameter value: {ex.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top rented vehicles");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets the number of rentals for each vehicle
    /// </summary>
    /// <returns>List of vehicles with their rental counts</returns>
    [HttpGet("rental-count-per-vehicle")]
    [ProducesResponseType(typeof(List<VehicleRentalCountResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VehicleRentalCountResponse>>> GetRentalCountPerVehicle()
    {
        try
        {
            logger.LogInformation("Getting rental count per vehicle");
            var result = await analyticsService.GetRentalCountPerVehicleAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rental count per vehicle");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets top N renters by total amount spent on rentals
    /// </summary>
    /// <param name="top">Number of top renters to return (default: 5)</param>
    /// <returns>List of top renters with total spent amounts</returns>
    [HttpGet("top-renters-by-spent")]
    [ProducesResponseType(typeof(List<RenterTotalSpentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RenterTotalSpentResponse>>> GetTopRentersByRentalSum([FromQuery, Range(1, 100)] int top = 5)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid top parameter: {Top}", top);
                return BadRequest("Invalid parameter value");
            }
            logger.LogInformation("Getting top {Top} renters by rental sum", top);
            var result = await analyticsService.GetTopRentersByRentalSumAsync(top);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Invalid top parameter: {Top}", top);
            return BadRequest($"Invalid parameter value: {ex.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting top renters by rental sum");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}