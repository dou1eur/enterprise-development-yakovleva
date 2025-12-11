using CarRentalService.Application.Contracts.ModelGeneration;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicle model generations
/// Provides CRUD operations for model generations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ModelGenerationsController(IModelGenerationService modelGenerationService, ILogger<ModelGenerationsController> logger) : ControllerBase
{
    /// <summary>
    /// Gets all model generations
    /// </summary>
    /// <returns>List of all model generations</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ModelGenerationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ModelGenerationResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Getting all model generations");
            var modelGenerations = await modelGenerationService.GetAllAsync();
            return Ok(modelGenerations);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all model generations");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Gets a specific model generation by ID
    /// </summary>
    /// <param name="id">The model generation ID</param>
    /// <returns>The model generation if found</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ModelGenerationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModelGenerationResponse>> Get(Guid id)
    {
        try
        {
            logger.LogInformation("Getting model generation {Id}", id);
            var modelGeneration = await modelGenerationService.GetAsync(id);
            return modelGeneration != null ? Ok(modelGeneration) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting model generation {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Creates a new model generation
    /// </summary>
    /// <param name="request">The model generation creation request</param>
    /// <returns>The created model generation</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ModelGenerationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModelGenerationResponse>> Create([FromBody] ModelGenerationRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Create), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Creating new model generation");
            var result = await modelGenerationService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Create));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating model generation");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Updates an existing model generation
    /// </summary>
    /// <param name="id">The model generation ID</param>
    /// <param name="request">The model generation update request</param>
    /// <returns>The updated model generation</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ModelGenerationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ModelGenerationResponse>> Update(Guid id, [FromBody] ModelGenerationRequest request)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Validation failed for {Operation}: {@Errors}",
                nameof(Update), ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest("Invalid request data");
        }

        try
        {
            logger.LogInformation("Updating model generation {Id}", id);
            var result = await modelGenerationService.UpdateAsync(id, request);
            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Business validation failed for {Operation}", nameof(Update));
            return BadRequest("Invalid request data");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating model generation {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Deletes a model generation
    /// </summary>
    /// <param name="id">The model generation ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting model generation {Id}", id);
            var result = await modelGenerationService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting model generation {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}