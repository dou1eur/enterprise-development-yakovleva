using CarRentalService.Application.Contracts.VehicleModel;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicle models
/// Provides CRUD operations for vehicle model entities
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehicleModelsController(
    ILogger<VehicleModelsController> logger,
    IVehicleModelService vehicleModelService)
    : CrudControllerBase<VehicleModelResponse, VehicleModelRequest, Guid>(logger)
{
    /// <summary>
    /// Extracts the identifier from a VehicleModelResponse DTO
    /// </summary>
    /// <param name="dto">The vehicle model response DTO</param>
    /// <returns>The vehicle model identifier</returns>
    protected override object GetId(VehicleModelResponse dto) => dto.Id;

    /// <summary>
    /// Gets the service instance for vehicle model CRUD operations
    /// </summary>
    /// <returns>The vehicle model service instance</returns>
    protected override dynamic GetService() => vehicleModelService;
}