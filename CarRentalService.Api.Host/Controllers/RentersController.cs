using CarRentalService.Application.Contracts.Renter;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing renters (customers)
/// Provides CRUD operations for renter entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController : ControllerBase
{
    private readonly IRenterService _renterService;

    /// <summary>
    /// Initializes a new instance of the RentersController class
    /// </summary>
    /// <param name="renterService">The renter service</param>
    public RentersController(IRenterService renterService)
    {
        _renterService = renterService;
    }

    /// <summary>
    /// Gets all renters
    /// </summary>
    /// <returns>List of all renters</returns>
    [HttpGet]
    public async Task<ActionResult<List<RenterResponse>>> GetAll()
    {
        var renters = await _renterService.GetAllAsync();
        return Ok(renters);
    }

    /// <summary>
    /// Gets a specific renter by ID
    /// </summary>
    /// <param name="id">The renter ID</param>
    /// <returns>The renter if found</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RenterResponse>> Get(Guid id)
    {
        var renter = await _renterService.GetAsync(id);
        return renter != null ? Ok(renter) : NotFound();
    }

    /// <summary>
    /// Creates a new renter
    /// </summary>
    /// <param name="request">The renter creation request</param>
    /// <returns>The created renter</returns>
    [HttpPost]
    public async Task<ActionResult<RenterResponse>> Create([FromBody] RenterRequest request)
    {
        var result = await _renterService.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing renter
    /// </summary>
    /// <param name="id">The renter ID</param>
    /// <param name="request">The renter update request</param>
    /// <returns>The updated renter</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RenterResponse>> Update(Guid id, [FromBody] RenterRequest request)
    {
        var result = await _renterService.UpdateAsync(id, request);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a renter
    /// </summary>
    /// <param name="id">The renter ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _renterService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}