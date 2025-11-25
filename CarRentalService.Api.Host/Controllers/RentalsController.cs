using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing rental operations
/// Provides endpoints for rental CRUD operations and analytical queries
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentalsController : CrudControllerBase<RentalDto, RentalCreateUpdateDto, Guid>
{
    private readonly IRentalService _rentalService;

    /// <summary>
    /// Initializes a new instance of the RentalsController class
    /// </summary>
    /// <param name="rentalService">The rental service</param>
    /// <param name="logger">The logger instance</param>
    public RentalsController(IRentalService rentalService, ILogger<RentalsController> logger)
        : base(logger)
    {
        _rentalService = rentalService;
    }

    /// <summary>
    /// Extracts the identifier from a rental DTO
    /// </summary>
    /// <param name="dto">The rental data transfer object</param>
    /// <returns>The rental identifier</returns>
    protected override object GetId(RentalDto dto) => dto.Id;

    /// <summary>
    /// Gets the rental service instance
    /// </summary>
    /// <returns>The rental service instance</returns>
    protected override dynamic GetService() => _rentalService;

    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The unique identifier of the renter</param>
    /// <returns>List of rentals for the specified renter</returns>
    [HttpGet("renter/{renterId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<RentalDto>> GetByRenterId(Guid renterId)
        => ExecuteWithLogging(nameof(GetByRenterId), () => Ok(_rentalService.GetByRenterId(renterId)));

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <returns>List of rentals for the specified vehicle</returns>
    [HttpGet("vehicle/{vehicleId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<RentalDto>> GetByVehicleId(Guid vehicleId)
        => ExecuteWithLogging(nameof(GetByVehicleId), () => Ok(_rentalService.GetByVehicleId(vehicleId)));

    /// <summary>
    /// Calculates the rental cost for a vehicle and duration
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <param name="durationHours">The rental duration in hours</param>
    /// <returns>The calculated rental cost</returns>
    [HttpGet("calculate-cost/{vehicleId}/{durationHours}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<decimal> CalculateRentalCost(Guid vehicleId, int durationHours)
        => ExecuteWithLogging(nameof(CalculateRentalCost), () =>
        {
            try
            {
                var result = _rentalService.CalculateRentalCost(vehicleId, durationHours);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        });
}