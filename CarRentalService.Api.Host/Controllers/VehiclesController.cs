using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicles
/// Provides CRUD operations for vehicle entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehiclesController(IVehicleService vehicleService, ILogger<VehiclesController> logger) : ControllerBase
{
    /// <summary>
    /// Gets all vehicles
    /// </summary>
    /// <returns>List of all vehicles</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<VehicleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VehicleResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Getting all vehicles");
            var vehicles = await vehicleService.GetAllAsync();
            return Ok(vehicles);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all vehicles");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets a specific vehicle by ID
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <returns>The vehicle if found</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleResponse>> Get(Guid id)
    {
        try
        {
            logger.LogInformation("Getting vehicle {Id}", id);
            var vehicle = await vehicleService.GetAsync(id);
            return vehicle != null ? Ok(vehicle) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting vehicle {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Creates a new vehicle
    /// </summary>
    /// <param name="request">The vehicle creation request</param>
    /// <returns>The created vehicle</returns>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleResponse>> Create([FromBody] VehicleRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Create), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Creating new vehicle");
            var result = await vehicleService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Create));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating vehicle");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Updates an existing vehicle
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <param name="request">The vehicle update request</param>
    /// <returns>The updated vehicle</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleResponse>> Update(Guid id, [FromBody] VehicleRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Update), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Updating vehicle {Id}", id);
            var result = await vehicleService.UpdateAsync(id, request);
            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Update));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating vehicle {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Deletes a vehicle
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting vehicle {Id}", id);
            var result = await vehicleService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting vehicle {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}