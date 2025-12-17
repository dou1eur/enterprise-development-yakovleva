using AutoMapper;
using CarRentalService.Application.Contracts.ModelGeneration;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using CarRentalService.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing model generations in the car rental system
/// Handles model generation operations with validation of related vehicle models
/// </summary>
/// <param name="modelGenerationRepository">Repository for model generations</param>
/// <param name="vehicleModelRepository">Repository for vehicle models</param>
/// <param name="mapper">AutoMapper instance</param>
/// <param name="logger">Logger instance</param>
public class ModelGenerationService(
    IModelGenerationRepository modelGenerationRepository,
    IVehicleModelRepository vehicleModelRepository,
    IMapper mapper,
    ILogger<ModelGenerationService> logger) : IModelGenerationService
{
    /// <summary>
    /// Creates a new model generation record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing model generation creation details</param>
    /// <returns>The created model generation response.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified vehicle model does not exist</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<ModelGenerationResponse> CreateAsync(ModelGenerationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating new model generation for vehicle model {VehicleModelId}",
            request.VehicleModelId);

        var vehicleModel = await vehicleModelRepository.GetByIdAsync(request.VehicleModelId);
        if (vehicleModel is null)
        {
            logger.LogWarning("Vehicle model with ID {VehicleModelId} not found", request.VehicleModelId);
            throw new ArgumentException($"Vehicle model with ID {request.VehicleModelId} does not exist.");
        }

        var modelGeneration = mapper.Map<ModelGeneration>(request);
        modelGeneration.Id = Guid.NewGuid();

        var createdModelGeneration = await modelGenerationRepository.AddAsync(modelGeneration);

        logger.LogInformation("Model generation created with ID {ModelGenerationId}", createdModelGeneration.Id);

        return mapper.Map<ModelGenerationResponse>(createdModelGeneration);
    }

    /// <summary>
    /// Retrieves a model generation by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model generation</param>
    /// <returns>The model generation response if found; otherwise, <c>null</c></returns>
    public async Task<ModelGenerationResponse?> GetAsync(Guid id)
    {
        logger.LogInformation("Retrieving model generation with ID {ModelGenerationId}", id);

        var modelGeneration = await modelGenerationRepository.GetByIdAsync(id);

        if (modelGeneration is null)
        {
            logger.LogWarning("Model generation with ID {ModelGenerationId} not found", id);
            return null;
        }

        return mapper.Map<ModelGenerationResponse>(modelGeneration);
    }

    /// <summary>
    /// Retrieves all model generation records from the system
    /// </summary>
    /// <returns>A list of all model generation responses</returns>
    public async Task<List<ModelGenerationResponse>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all model generations");

        var modelGenerations = await modelGenerationRepository.GetAllAsync();

        logger.LogDebug("Found {Count} model generations", modelGenerations.Count);

        return mapper.Map<List<ModelGenerationResponse>>(modelGenerations);
    }

    /// <summary>
    /// Updates an existing model generation's information
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to update.</param>
    /// <param name="request">Data transfer object containing updated model generation details</param>
    /// <returns>The updated model generation response if successful; otherwise, <c>null</c></returns>
    /// <exception cref="ArgumentException">Thrown when the specified vehicle model does not exist</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<ModelGenerationResponse?> UpdateAsync(Guid id, ModelGenerationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Updating model generation with ID {ModelGenerationId}", id);

        var existingModelGeneration = await modelGenerationRepository.GetByIdAsync(id);
        if (existingModelGeneration is null)
        {
            logger.LogWarning("Model generation with ID {ModelGenerationId} not found for update", id);
            return null;
        }

        var vehicleModel = await vehicleModelRepository.GetByIdAsync(request.VehicleModelId);
        if (vehicleModel is null)
        {
            logger.LogWarning("Vehicle model with ID {VehicleModelId} not found for update", request.VehicleModelId);
            throw new ArgumentException($"Vehicle model with ID {request.VehicleModelId} does not exist.");
        }

        mapper.Map(request, existingModelGeneration);
        var updatedModelGeneration = await modelGenerationRepository.UpdateAsync(existingModelGeneration);

        logger.LogInformation("Model generation with ID {ModelGenerationId} updated successfully", id);

        return mapper.Map<ModelGenerationResponse>(updatedModelGeneration);
    }

    /// <summary>
    /// Deletes a model generation record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete</param>
    /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c></returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting model generation with ID {ModelGenerationId}", id);

        var result = await modelGenerationRepository.DeleteAsync(id);

        if (result)
        {
            logger.LogInformation("Model generation with ID {ModelGenerationId} deleted successfully", id);
        }
        else
        {
            logger.LogWarning("Model generation with ID {ModelGenerationId} not found for deletion", id);
        }

        return result;
    }
}