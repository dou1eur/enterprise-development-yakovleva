using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicles
/// Provides CRUD operations for vehicle entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehiclesController(
    ILogger<VehiclesController> logger,
    IVehicleService vehicleService)
    : CrudControllerBase<VehicleResponse, VehicleRequest, Guid>(logger)
{
    /// <summary>
    /// Extracts the identifier from a VehicleResponse DTO
    /// </summary>
    /// <param name="dto">The vehicle response DTO</param>
    /// <returns>The vehicle identifier</returns>
    protected override object GetId(VehicleResponse dto) => dto.Id;

    /// <summary>
    /// Gets the service instance for vehicle CRUD operations
    /// </summary>
    /// <returns>The vehicle service instance</returns>
    protected override dynamic GetService() => vehicleService;
}