using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing vehicle operations
/// Provides endpoints for vehicle CRUD operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : CrudControllerBase<VehicleDto, CreateVehicleRequest, UpdateVehicleRequest, Guid>
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
}