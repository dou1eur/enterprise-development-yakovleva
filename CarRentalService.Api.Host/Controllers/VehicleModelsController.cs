using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing vehicle model operations
/// Provides endpoints for vehicle model CRUD operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehicleModelsController : CrudControllerBase<VehicleModelDto, CreateVehicleModelRequest, UpdateVehicleModelRequest, Guid>
{
    private readonly IVehicleModelService _vehicleModelService;

    /// <summary>
    /// Initializes a new instance of the VehicleModelsController class
    /// </summary>
    /// <param name="vehicleModelService">The vehicle model service</param>
    /// <param name="logger">The logger instance</param>
    public VehicleModelsController(IVehicleModelService vehicleModelService, ILogger<VehicleModelsController> logger)
        : base(logger)
    {
        _vehicleModelService = vehicleModelService;
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
    protected override dynamic GetService() => _vehicleModelService;
}