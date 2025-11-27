using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing vehicle model operations
/// Provides endpoints for vehicle model CRUD operations and analytical queries
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehicleModelsController : CrudControllerBase<VehicleModelDto, VehicleModelCreateUpdateDto, Guid>
{
    private readonly IVehicleModelService _modelService;

    /// <summary>
    /// Initializes a new instance of the VehicleModelsController class
    /// </summary>
    /// <param name="modelService">The vehicle model service</param>
    /// <param name="logger">The logger instance</param>
    public VehicleModelsController(IVehicleModelService modelService, ILogger<VehicleModelsController> logger)
        : base(logger)
    {
        _modelService = modelService;
    }

    /// <summary>
    /// Extracts the identifier from a vehicle model DTO
    /// </summary>
    /// <param name="dto">The vehicle model data transfer object</param>
    /// <returns>The model identifier</returns>
    protected override object GetId(VehicleModelDto dto) => dto.Id;

    /// <summary>
    /// Gets the vehicle model service instance
    /// </summary>
    /// <returns>The vehicle model service instance</returns>
    protected override dynamic GetService() => _modelService;

    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model if found</returns>
    [HttpGet("name/{name}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<VehicleModelDto> GetByName(string name)
        => ExecuteWithLogging(nameof(GetByName), () =>
        {
            var model = _modelService.GetByName(name);
            if (model == null)
                return NotFound($"Vehicle model with name {name} not found");
            return Ok(model);
        });

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle models with the specified body type</returns>
    [HttpGet("body-type/{bodyType}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<VehicleModelDto>> GetByBodyType(BodyType bodyType)
        => ExecuteWithLogging(nameof(GetByBodyType), () => Ok(_modelService.GetByBodyType(bodyType)));

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle models with the specified vehicle class</returns>
    [HttpGet("vehicle-class/{vehicleClass}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<VehicleModelDto>> GetByVehicleClass(VehicleClass vehicleClass)
        => ExecuteWithLogging(nameof(GetByVehicleClass), () => Ok(_modelService.GetByVehicleClass(vehicleClass)));
}