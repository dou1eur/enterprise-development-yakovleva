using CarRentalService.Application.Contracts.VehicleModel;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicle models
/// Provides CRUD operations for vehicle model entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehicleModelsController(IVehicleModelService vehicleModelService, ILogger<VehicleModelsController> logger) : ControllerBase
{
    /// <summary>
    /// Gets all vehicle models
    /// </summary>
    /// <returns>List of all vehicle models</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<VehicleModelResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<VehicleModelResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Getting all vehicle models");
            var vehicleModels = await vehicleModelService.GetAllAsync();
            return Ok(vehicleModels);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all vehicle models");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets a specific vehicle model by ID
    /// </summary>
    /// <param name="id">The vehicle model ID</param>
    /// <returns>The vehicle model if found</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VehicleModelResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleModelResponse>> Get(Guid id)
    {
        try
        {
            logger.LogInformation("Getting vehicle model {Id}", id);
            var vehicleModel = await vehicleModelService.GetAsync(id);
            return vehicleModel != null ? Ok(vehicleModel) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting vehicle model {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Creates a new vehicle model
    /// </summary>
    /// <param name="request">The vehicle model creation request</param>
    /// <returns>The created vehicle model</returns>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleModelResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleModelResponse>> Create([FromBody] VehicleModelRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Create), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Creating new vehicle model");
            var result = await vehicleModelService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Create));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating vehicle model");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Updates an existing vehicle model
    /// </summary>
    /// <param name="id">The vehicle model ID</param>
    /// <param name="request">The vehicle model update request</param>
    /// <returns>The updated vehicle model</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(VehicleModelResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VehicleModelResponse>> Update(Guid id, [FromBody] VehicleModelRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Update), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Updating vehicle model {Id}", id);
            var result = await vehicleModelService.UpdateAsync(id, request);
            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Update));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating vehicle model {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Deletes a vehicle model
    /// </summary>
    /// <param name="id">The vehicle model ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting vehicle model {Id}", id);
            var result = await vehicleModelService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting vehicle model {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}