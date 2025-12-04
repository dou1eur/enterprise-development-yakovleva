using CarRentalService.Application.Contracts.ModelGeneration;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicle model generations
/// Provides CRUD operations for model generations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ModelGenerationsController : ControllerBase
{
    private readonly IModelGenerationService _modelGenerationService;

    /// <summary>
    /// Initializes a new instance of the ModelGenerationsController class
    /// </summary>
    /// <param name="modelGenerationService">The model generation service</param>
    public ModelGenerationsController(IModelGenerationService modelGenerationService)
    {
        _modelGenerationService = modelGenerationService;
    }

    /// <summary>
    /// Gets all model generations
    /// </summary>
    /// <returns>List of all model generations</returns>
    [HttpGet]
    public async Task<ActionResult<List<ModelGenerationResponse>>> GetAll()
    {
        var modelGenerations = await _modelGenerationService.GetAllAsync();
        return Ok(modelGenerations);
    }

    /// <summary>
    /// Gets a specific model generation by ID
    /// </summary>
    /// <param name="id">The model generation ID</param>
    /// <returns>The model generation if found</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ModelGenerationResponse>> Get(Guid id)
    {
        var modelGeneration = await _modelGenerationService.GetAsync(id);
        return modelGeneration != null ? Ok(modelGeneration) : NotFound();
    }

    /// <summary>
    /// Creates a new model generation
    /// </summary>
    /// <param name="request">The model generation creation request</param>
    /// <returns>The created model generation</returns>
    [HttpPost]
    public async Task<ActionResult<ModelGenerationResponse>> Create([FromBody] ModelGenerationRequest request)
    {
        var result = await _modelGenerationService.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing model generation
    /// </summary>
    /// <param name="id">The model generation ID</param>
    /// <param name="request">The model generation update request</param>
    /// <returns>The updated model generation</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ModelGenerationResponse>> Update(Guid id, [FromBody] ModelGenerationRequest request)
    {
        var result = await _modelGenerationService.UpdateAsync(id, request);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a model generation
    /// </summary>
    /// <param name="id">The model generation ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _modelGenerationService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}