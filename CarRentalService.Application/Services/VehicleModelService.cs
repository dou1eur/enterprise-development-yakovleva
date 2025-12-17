using AutoMapper;
using CarRentalService.Application.Contracts.VehicleModel;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using CarRentalService.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing vehicle models in the car rental system
/// Handles all CRUD operations for vehicle model entities
/// </summary>
/// <param name="vehicleModelRepository">Repository for vehicle models</param>
/// <param name="mapper">AutoMapper instance</param>
/// <param name="logger">Logger instance</param>
public class VehicleModelService(
    IVehicleModelRepository vehicleModelRepository,
    IMapper mapper,
    ILogger<VehicleModelService> logger) : IVehicleModelService
{
    /// <summary>
    /// Creates a new vehicle model record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing vehicle model creation details</param>
    /// <returns>The created vehicle model response</returns>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<VehicleModelResponse> CreateAsync(VehicleModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating new vehicle model: {ModelName}", request.Name);

        var vehicleModel = mapper.Map<VehicleModel>(request);
        vehicleModel.Id = Guid.NewGuid();

        var createdVehicleModel = await vehicleModelRepository.AddAsync(vehicleModel);

        logger.LogInformation("Vehicle model created with ID {ModelId}", createdVehicleModel.Id);

        return mapper.Map<VehicleModelResponse>(createdVehicleModel);
    }

    /// <summary>
    /// Retrieves a vehicle model by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model</param>
    /// <returns>The vehicle model response if found; otherwise, null</returns>
    public async Task<VehicleModelResponse?> GetAsync(Guid id)
    {
        logger.LogInformation("Retrieving vehicle model with ID {ModelId}", id);

        var vehicleModel = await vehicleModelRepository.GetByIdAsync(id);

        if (vehicleModel is null)
        {
            logger.LogWarning("Vehicle model with ID {ModelId} not found", id);
            return null;
        }

        return mapper.Map<VehicleModelResponse>(vehicleModel);
    }

    /// <summary>
    /// Retrieves all vehicle model records from the system
    /// </summary>
    /// <returns>A list of all vehicle model responses</returns>
    public async Task<List<VehicleModelResponse>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all vehicle models");

        var vehicleModels = await vehicleModelRepository.GetAllAsync();

        logger.LogDebug("Found {Count} vehicle models", vehicleModels.Count);

        return mapper.Map<List<VehicleModelResponse>>(vehicleModels);
    }

    /// <summary>
    /// Updates an existing vehicle model's information
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to update</param>
    /// <param name="request">Data transfer object containing updated vehicle model details</param>
    /// <returns>The updated vehicle model response if successful; otherwise, null</returns>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<VehicleModelResponse?> UpdateAsync(Guid id, VehicleModelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Updating vehicle model with ID {ModelId}", id);

        var existingVehicleModel = await vehicleModelRepository.GetByIdAsync(id);
        if (existingVehicleModel is null)
        {
            logger.LogWarning("Vehicle model with ID {ModelId} not found for update", id);
            return null;
        }

        mapper.Map(request, existingVehicleModel);
        var updatedVehicleModel = await vehicleModelRepository.UpdateAsync(existingVehicleModel);

        logger.LogInformation("Vehicle model with ID {ModelId} updated successfully", id);

        return mapper.Map<VehicleModelResponse>(updatedVehicleModel);
    }

    /// <summary>
    /// Deletes a vehicle model record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete</param>
    /// <returns>True if deletion was successful; otherwise, false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting vehicle model with ID {ModelId}", id);

        var result = await vehicleModelRepository.DeleteAsync(id);

        if (result)
        {
            logger.LogInformation("Vehicle model with ID {ModelId} deleted successfully", id);
        }
        else
        {
            logger.LogWarning("Vehicle model with ID {ModelId} not found for deletion", id);
        }

        return result;
    }
}