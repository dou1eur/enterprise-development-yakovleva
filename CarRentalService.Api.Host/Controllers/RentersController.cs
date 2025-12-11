using CarRentalService.Application.Contracts.Renter;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing renters (customers)
/// Provides CRUD operations for renter entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController(IRenterService renterService, ILogger<RentersController> logger) : ControllerBase
{
    /// <summary>
    /// Gets all renters
    /// </summary>
    /// <returns>List of all renters</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<RenterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RenterResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Getting all renters");
            var renters = await renterService.GetAllAsync();
            return Ok(renters);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all renters");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets a specific renter by ID
    /// </summary>
    /// <param name="id">The renter ID</param>
    /// <returns>The renter if found</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RenterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterResponse>> Get(Guid id)
    {
        try
        {
            logger.LogInformation("Getting renter {Id}", id);
            var renter = await renterService.GetAsync(id);
            return renter != null ? Ok(renter) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting renter {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Creates a new renter
    /// </summary>
    /// <param name="request">The renter creation request</param>
    /// <returns>The created renter</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RenterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterResponse>> Create([FromBody] RenterRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Create), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Creating new renter");
            var result = await renterService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Create));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating renter");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Updates an existing renter
    /// </summary>
    /// <param name="id">The renter ID</param>
    /// <param name="request">The renter update request</param>
    /// <returns>The updated renter</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RenterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RenterResponse>> Update(Guid id, [FromBody] RenterRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Update), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Updating renter {Id}", id);
            var result = await renterService.UpdateAsync(id, request);
            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Update));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating renter {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Deletes a renter
    /// </summary>
    /// <param name="id">The renter ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting renter {Id}", id);
            var result = await renterService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting renter {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}