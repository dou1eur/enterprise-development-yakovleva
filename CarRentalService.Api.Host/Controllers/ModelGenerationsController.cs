using CarRentalService.Application.Contracts.ModelGeneration;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// Controller for managing vehicle model generations
/// Provides CRUD operations for model generations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ModelGenerationsController(
    ILogger<ModelGenerationsController> logger,
    IModelGenerationService modelGenerationService)
    : CrudControllerBase<ModelGenerationResponse, ModelGenerationRequest, ModelGenerationRequest, Guid>(logger)
{
    /// <summary>
    /// Extracts the identifier from a ModelGenerationResponse DTO
    /// </summary>
    /// <param name="dto">The model generation response DTO</param>
    /// <returns>The model generation identifier</returns>
    protected override object GetId(ModelGenerationResponse dto) => dto.Id;

    /// <summary>
    /// Gets the service instance for model generation CRUD operations
    /// </summary>
    /// <returns>The model generation service instance</returns>
    protected override dynamic GetService() => modelGenerationService;
}