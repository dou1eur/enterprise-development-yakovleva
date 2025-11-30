using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Application.Mappings;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing model generations in the car rental system
/// Handles model generation operations with validation of related vehicle models
/// </summary>
public class ModelGenerationService : IModelGenerationService
{
    private readonly IModelGenerationRepository _modelGenerationRepository;
    private readonly IVehicleModelRepository _vehicleModelRepository;

    /// <summary>
    /// Initializes a new instance of ModelGenerationService
    /// </summary>
    /// <param name="modelGenerationRepository">The model generation repository for data access</param>
    /// <param name="vehicleModelRepository">The vehicle model repository for validation</param>
    public ModelGenerationService(
        IModelGenerationRepository modelGenerationRepository,
        IVehicleModelRepository vehicleModelRepository)
    {
        _modelGenerationRepository = modelGenerationRepository;
        _vehicleModelRepository = vehicleModelRepository;
    }

    /// <summary>
    /// Creates a new model generation record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing model generation creation details</param>
    /// <returns>The created model generation data transfer object</returns>
    /// <exception cref="ArgumentException">Thrown when the specified vehicle model does not exist</exception>
    public async Task<ModelGenerationDto> CreateAsync(CreateModelGenerationRequest request)
    {
        var vehicleModel = await _vehicleModelRepository.GetByIdAsync(request.VehicleModelId);
        if (vehicleModel == null)
            throw new ArgumentException($"Vehicle model with ID {request.VehicleModelId} does not exist");

        var modelGeneration = request.ToDomain();
        var createdModelGeneration = await _modelGenerationRepository.AddAsync(modelGeneration);
        return createdModelGeneration.ToDto();
    }

    /// <summary>
    /// Retrieves a specific model generation by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model generation</param>
    /// <returns>The model generation data transfer object if found; otherwise, null</returns>
    public async Task<ModelGenerationDto?> GetAsync(Guid id)
    {
        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(id);
        return modelGeneration?.ToDto();
    }

    /// <summary>
    /// Retrieves all model generation records from the system
    /// </summary>
    /// <returns>List of all model generation data transfer objects</returns>
    public async Task<List<ModelGenerationDto>> GetAllAsync()
    {
        var modelGenerations = await _modelGenerationRepository.GetAllAsync();
        return modelGenerations.ConvertAll(mg => mg.ToDto());
    }

    /// <summary>
    /// Updates an existing model generation's information
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to update</param>
    /// <param name="request">Data transfer object containing updated model generation details</param>
    /// <returns>The updated model generation data transfer object if successful; otherwise, null</returns>
    /// <exception cref="ArgumentException">Thrown when the specified vehicle model does not exist</exception>
    public async Task<ModelGenerationDto?> UpdateAsync(Guid id, UpdateModelGenerationRequest request)
    {
        var existingModelGeneration = await _modelGenerationRepository.GetByIdAsync(id);
        if (existingModelGeneration == null)
            return null;

        var vehicleModel = await _vehicleModelRepository.GetByIdAsync(request.VehicleModelId);
        if (vehicleModel == null)
            throw new ArgumentException($"Vehicle model with ID {request.VehicleModelId} does not exist");

        existingModelGeneration.Year = request.Year;
        existingModelGeneration.EngineVolume = request.EngineVolume;
        existingModelGeneration.Transmission = request.Transmission;
        existingModelGeneration.RentalPricePerHour = request.RentalPricePerHour;
        existingModelGeneration.VehicleModelId = request.VehicleModelId;

        var updatedModelGeneration = await _modelGenerationRepository.UpdateAsync(existingModelGeneration);
        return updatedModelGeneration.ToDto();
    }

    /// <summary>
    /// Deletes a model generation record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the model generation to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _modelGenerationRepository.DeleteAsync(id);
    }
}