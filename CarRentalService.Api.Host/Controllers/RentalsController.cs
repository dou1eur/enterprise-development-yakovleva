using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing rentals
/// Provides CRUD operations for rental entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;

    /// <summary>
    /// Initializes a new instance of the RentalsController class
    /// </summary>
    /// <param name="rentalService">The rental service</param>
    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    /// <summary>
    /// Gets all rentals
    /// </summary>
    /// <returns>List of all rentals</returns>
    [HttpGet]
    public async Task<ActionResult<List<RentalResponse>>> GetAll()
    {
        var rentals = await _rentalService.GetAllAsync();
        return Ok(rentals);
    }

    /// <summary>
    /// Gets a specific rental by ID
    /// </summary>
    /// <param name="id">The rental ID</param>
    /// <returns>The rental if found</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RentalResponse>> Get(Guid id)
    {
        var rental = await _rentalService.GetAsync(id);
        return rental != null ? Ok(rental) : NotFound();
    }

    /// <summary>
    /// Creates a new rental
    /// </summary>
    /// <param name="request">The rental creation request</param>
    /// <returns>The created rental</returns>
    [HttpPost]
    public async Task<ActionResult<RentalResponse>> Create([FromBody] RentalRequest request)
    {
        var result = await _rentalService.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Updates an existing rental
    /// </summary>
    /// <param name="id">The rental ID</param>
    /// <param name="request">The rental update request</param>
    /// <returns>The updated rental</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<RentalResponse>> Update(Guid id, [FromBody] RentalRequest request)
    {
        var result = await _rentalService.UpdateAsync(id, request);
        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Deletes a rental
    /// </summary>
    /// <param name="id">The rental ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _rentalService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}