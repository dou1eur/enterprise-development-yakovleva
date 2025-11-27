using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing vehicle generation operations
/// Provides endpoints for vehicle generation CRUD operations and analytical queries
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehicleGenerationsController : CrudControllerBase<VehicleGenerationDto, VehicleGenerationCreateUpdateDto, Guid>
{
    private readonly IVehicleGenerationService _generationService;

    /// <summary>
    /// Initializes a new instance of the VehicleGenerationsController class
    /// </summary>
    /// <param name="generationService">The vehicle generation service</param>
    /// <param name="logger">The logger instance</param>
    public VehicleGenerationsController(IVehicleGenerationService generationService, ILogger<VehicleGenerationsController> logger)
        : base(logger)
    {
        _generationService = generationService;
    }

    /// <summary>
    /// Extracts the identifier from a vehicle generation DTO
    /// </summary>
    /// <param name="dto">The vehicle generation data transfer object</param>
    /// <returns>The generation identifier</returns>
    protected override object GetId(VehicleGenerationDto dto) => dto.Id;

    /// <summary>
    /// Gets the vehicle generation service instance
    /// </summary>
    /// <returns>The vehicle generation service instance</returns>
    protected override dynamic GetService() => _generationService;

    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The unique identifier of the vehicle model</param>
    /// <returns>List of generations for the specified model</returns>
    [HttpGet("model/{modelId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<VehicleGenerationDto>> GetByModelId(Guid modelId)
        => ExecuteWithLogging(nameof(GetByModelId), () => Ok(_generationService.GetByModelId(modelId)));

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generations within the specified year range</returns>
    [HttpGet("year-range/{startYear}/{endYear}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<List<VehicleGenerationDto>> GetByYearRange(int startYear, int endYear)
        => ExecuteWithLogging(nameof(GetByYearRange), () => Ok(_generationService.GetByYearRange(startYear, endYear)));
}