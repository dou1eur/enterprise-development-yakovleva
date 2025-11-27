using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing vehicle operations
/// Provides endpoints for vehicle CRUD operations and analytical queries
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : CrudControllerBase<VehicleDto, VehicleCreateUpdateDto, Guid>
{
    private readonly IVehicleService _vehicleService;

    /// <summary>
    /// Initializes a new instance of the VehiclesController class
    /// </summary>
    /// <param name="vehicleService">The vehicle service</param>
    /// <param name="logger">The logger instance</param>
    public VehiclesController(IVehicleService vehicleService, ILogger<VehiclesController> logger)
        : base(logger)
    {
        _vehicleService = vehicleService;
    }

    /// <summary>
    /// Extracts the identifier from a vehicle DTO
    /// </summary>
    /// <param name="dto">The vehicle data transfer object</param>
    /// <returns>The vehicle identifier</returns>
    protected override object GetId(VehicleDto dto) => dto.Id;

    /// <summary>
    /// Gets the vehicle service instance
    /// </summary>
    /// <returns>The vehicle service instance</returns>
    protected override dynamic GetService() => _vehicleService;

    /// <summary>
    /// Retrieves a vehicle by it's license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle if found</returns>
    [HttpGet("license-plate/{licensePlate}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<VehicleDto> GetByLicensePlate(string licensePlate)
        => ExecuteWithLogging(nameof(GetByLicensePlate), () =>
        {
            var vehicle = _vehicleService.GetByLicensePlate(licensePlate);
            if (vehicle == null)
                return NotFound($"Vehicle with license plate {licensePlate} not found");
            return Ok(vehicle);
        });

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The unique identifier of the vehicle model</param>
    /// <returns>List of vehicles for the specified model</returns>
    [HttpGet("model/{modelId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<VehicleDto>> GetByModel(Guid modelId)
        => ExecuteWithLogging(nameof(GetByModel), () => Ok(_vehicleService.GetByModel(modelId)));
}