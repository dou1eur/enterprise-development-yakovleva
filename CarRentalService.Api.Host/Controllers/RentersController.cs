using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing renter operations
/// Provides endpoints for renter CRUD operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentersController : CrudControllerBase<RenterDto, RenterCreateUpdateDto, Guid>
{
    private readonly IRenterService _renterService;

    /// <summary>
    /// Initializes a new instance of the RentersController class
    /// </summary>
    /// <param name="renterService">The renter service</param>
    /// <param name="logger">The logger instance</param>
    public RentersController(IRenterService renterService, ILogger<RentersController> logger)
        : base(logger)
    {
        _renterService = renterService;
    }

    /// <summary>
    /// Extracts the identifier from a renter DTO
    /// </summary>
    /// <param name="dto">The renter data transfer object</param>
    /// <returns>The renter identifier</returns>
    protected override object GetId(RenterDto dto) => dto.Id;

    /// <summary>
    /// Gets the renter service instance
    /// </summary>
    /// <returns>The renter service instance</returns>
    protected override dynamic GetService() => _renterService;

    /// <summary>
    /// Retrieves a renter by driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter if found</returns>
    [HttpGet("license/{licenseNumber}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<RenterDto> GetByLicenseNumber(string licenseNumber)
        => ExecuteWithLogging(nameof(GetByLicenseNumber), () =>
        {
            var renter = _renterService.GetByLicenseNumber(licenseNumber);
            if (renter == null)
                return NotFound($"Renter with license number {licenseNumber} not found");
            return Ok(renter);
        });
}