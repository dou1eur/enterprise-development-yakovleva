using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CarRentalService.Api.Host.Controllers;

/// <summary>
/// API controller for managing model generation operations
/// Provides endpoints for model generation CRUD operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ModelGenerationsController : CrudControllerBase<ModelGenerationDto, CreateModelGenerationRequest, UpdateModelGenerationRequest, Guid>
{
    private readonly IModelGenerationService _modelGenerationService;

    /// <summary>
    /// Initializes a new instance of the ModelGenerationsController class
    /// </summary>
    /// <param name="modelGenerationService">The model generation service</param>
    /// <param name="logger">The logger instance</param>
    public ModelGenerationsController(IModelGenerationService modelGenerationService, ILogger<ModelGenerationsController> logger)
        : base(logger)
    {
        _modelGenerationService = modelGenerationService;
    }

    /// <summary>
    /// Extracts the identifier from a model generation DTO
    /// </summary>
    /// <param name="dto">The model generation data transfer object</param>
    /// <returns>The generation identifier</returns>
    protected override object GetId(ModelGenerationDto dto) => dto.Id;

    /// <summary>
    /// Gets the model generation service instance
    /// </summary>
    /// <returns>The model generation service instance</returns>
    protected override dynamic GetService() => _modelGenerationService;
}