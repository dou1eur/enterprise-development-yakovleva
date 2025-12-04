using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicles
/// Provides CRUD operations for vehicle entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    /// <summary>
    /// Initializes a new instance of the VehiclesController class
    /// </summary>
    /// <param name="vehicleService">The vehicle service</param>
    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    /// <summary>
    /// Gets all vehicles
    /// </summary>
    /// <returns>List of all vehicles</returns>
    [HttpGet]
    public async Task<ActionResult<List<VehicleResponse>>> GetAll()
    {
        var vehicles = await _vehicleService.GetAllAsync();
        return Ok(vehicles);
    }

    /// <summary>
    /// Gets a specific vehicle by ID
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <returns>The vehicle if found</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleResponse>> Get(Guid id)
    {
        var vehicle = await _vehicleService.GetAsync(id);
        return vehicle != null ? Ok(vehicle) : NotFound();
    }

    /// <summary>
    /// Creates a new vehicle
    /// </summary>
    /// <param name="request">The vehicle creation request</param>
    /// <returns>The created vehicle</returns>
    [HttpPost]
    public async Task<ActionResult<VehicleResponse>> Create([FromBody] VehicleRequest request)
    {
        var result = await _vehicleService.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing vehicle
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <param name="request">The vehicle update request</param>
    /// <returns>The updated vehicle</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<VehicleResponse>> Update(Guid id, [FromBody] VehicleRequest request)
    {
        var result = await _vehicleService.UpdateAsync(id, request);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a vehicle
    /// </summary>
    /// <param name="id">The vehicle ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _vehicleService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}