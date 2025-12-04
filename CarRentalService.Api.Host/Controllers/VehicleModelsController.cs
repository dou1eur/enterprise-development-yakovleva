using CarRentalService.Application.Contracts.VehicleModel;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicle models
/// Provides CRUD operations for vehicle model entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehicleModelsController : ControllerBase
{
    private readonly IVehicleModelService _vehicleModelService;

    /// <summary>
    /// Initializes a new instance of the VehicleModelsController class
    /// </summary>
    /// <param name="vehicleModelService">The vehicle model service</param>
    public VehicleModelsController(IVehicleModelService vehicleModelService)
    {
        _vehicleModelService = vehicleModelService;
    }

    /// <summary>
    /// Gets all vehicle models
    /// </summary>
    /// <returns>List of all vehicle models</returns>
    [HttpGet]
    public async Task<ActionResult<List<VehicleModelResponse>>> GetAll()
    {
        var vehicleModels = await _vehicleModelService.GetAllAsync();
        return Ok(vehicleModels);
    }

    /// <summary>
    /// Gets a specific vehicle model by ID
    /// </summary>
    /// <param name="id">The vehicle model ID</param>
    /// <returns>The vehicle model if found</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleModelResponse>> Get(Guid id)
    {
        var vehicleModel = await _vehicleModelService.GetAsync(id);
        return vehicleModel != null ? Ok(vehicleModel) : NotFound();
    }

    /// <summary>
    /// Creates a new vehicle model
    /// </summary>
    /// <param name="request">The vehicle model creation request</param>
    /// <returns>The created vehicle model</returns>
    [HttpPost]
    public async Task<ActionResult<VehicleModelResponse>> Create([FromBody] VehicleModelRequest request)
    {
        var result = await _vehicleModelService.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing vehicle model
    /// </summary>
    /// <param name="id">The vehicle model ID</param>
    /// <param name="request">The vehicle model update request</param>
    /// <returns>The updated vehicle model</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<VehicleModelResponse>> Update(Guid id, [FromBody] VehicleModelRequest request)
    {
        var result = await _vehicleModelService.UpdateAsync(id, request);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a vehicle model
    /// </summary>
    /// <param name="id">The vehicle model ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _vehicleModelService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}