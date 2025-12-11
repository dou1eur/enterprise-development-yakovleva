using CarRentalService.Application.Contracts.ModelGeneration;
using CarRentalService.Application.Interfaces;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Application.Mappings;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing model generations in the car rental system
/// Handles model generation operations with validation of related vehicle models
/// </summary>
public class ModelGenerationService(
    IModelGenerationRepository modelGenerationRepository,
    IVehicleModelRepository vehicleModelRepository)
    : IModelGenerationService
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
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var vehicleModel = await vehicleModelRepository.GetByIdAsync(request.VehicleModelId);
        if (vehicleModel == null)
            throw new ArgumentException($"Vehicle model with ID {request.VehicleModelId} does not exist.", nameof(request.VehicleModelId));

        var modelGeneration = request.ToDomain();
        var createdModelGeneration = await modelGenerationRepository.AddAsync(modelGeneration);

        return createdModelGeneration.ToResponse();
    }

    /// <summary>
    /// Retrieves a model generation by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model generation</param>
    /// <returns>The model generation response if found; otherwise, <c>null</c></returns>
    public async Task<ModelGenerationResponse?> GetAsync(Guid id)
    {
        var modelGeneration = await modelGenerationRepository.GetByIdAsync(id);
        return modelGeneration?.ToResponse();
    }

    /// <summary>
    /// Retrieves all model generation records from the system
    /// </summary>
    /// <returns>A list of all model generation responses</returns>
    public async Task<List<ModelGenerationResponse>> GetAllAsync()
    {
        var modelGenerations = await modelGenerationRepository.GetAllAsync();
        return modelGenerations.ToResponseList();
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
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var existingModelGeneration = await modelGenerationRepository.GetByIdAsync(id);
        if (existingModelGeneration == null)
            return null;

        var vehicleModel = await vehicleModelRepository.GetByIdAsync(request.VehicleModelId);
        if (vehicleModel == null)
            throw new ArgumentException($"Vehicle model with ID {request.VehicleModelId} does not exist.", nameof(request.VehicleModelId));

        existingModelGeneration.Year = request.Year;
        existingModelGeneration.EngineVolume = request.EngineVolume;
        existingModelGeneration.Transmission = request.Transmission;
        existingModelGeneration.RentalPricePerHour = request.RentalPricePerHour;
        existingModelGeneration.VehicleModelId = request.VehicleModelId;

        var updatedModelGeneration = await modelGenerationRepository.UpdateAsync(existingModelGeneration);
        return updatedModelGeneration.ToResponse();
    }

    /// <summary>
    /// Deletes a model generation record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete</param>
    /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c></returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await modelGenerationRepository.DeleteAsync(id);
    }
}