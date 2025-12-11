using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing rentals
/// Provides CRUD operations for rental entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalsController(IRentalService rentalService, ILogger<RentalsController> logger) : ControllerBase
{
    /// <summary>
    /// Gets all rentals
    /// </summary>
    /// <returns>List of all rentals</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<RentalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RentalResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Getting all rentals");
            var rentals = await rentalService.GetAllAsync();
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all rentals");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets a specific rental by ID
    /// </summary>
    /// <param name="id">The rental ID</param>
    /// <returns>The rental if found</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalResponse>> Get(Guid id)
    {
        try
        {
            logger.LogInformation("Getting rental {Id}", id);
            var rental = await rentalService.GetAsync(id);
            return rental != null ? Ok(rental) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rental {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Creates a new rental
    /// </summary>
    /// <param name="request">The rental creation request</param>
    /// <returns>The created rental</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalResponse>> Create([FromBody] RentalRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Create), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Creating new rental");
            var result = await rentalService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Create));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating rental");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Updates an existing rental
    /// </summary>
    /// <param name="id">The rental ID</param>
    /// <param name="request">The rental update request</param>
    /// <returns>The updated rental</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RentalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalResponse>> Update(Guid id, [FromBody] RentalRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Update), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Updating rental {Id}", id);
            var result = await rentalService.UpdateAsync(id, request);
            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Update));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating rental {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Deletes a rental
    /// </summary>
    /// <param name="id">The rental ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting rental {Id}", id);
            var result = await rentalService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting rental {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}