using AutoMapper;
using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using CarRentalService.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing vehicles in the car rental system
/// Handles vehicle operations with validation of related entities
/// </summary>
/// <param name="vehicleRepository">Repository for vehicles</param>
/// <param name="modelGenerationRepository">Repository for model generations</param>
/// <param name="mapper">AutoMapper instance</param>
/// <param name="logger">Logger instance</param>
public class VehicleService(
    IVehicleRepository vehicleRepository,
    IModelGenerationRepository modelGenerationRepository,
    IMapper mapper,
    ILogger<VehicleService> logger) : IVehicleService
{
    /// <summary>
    /// Creates a new vehicle record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing vehicle creation details</param>
    /// <returns>The created vehicle response</returns>
    /// <exception cref="ArgumentException">Thrown when the model generation does not exist</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<VehicleResponse> CreateAsync(VehicleRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating new vehicle with license plate {LicensePlate}", request.LicensePlate);

        var _ = await modelGenerationRepository.GetByIdAsync(request.ModelGenerationId)
            ?? throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist.");

        var vehicle = mapper.Map<Vehicle>(request);
        vehicle.Id = Guid.NewGuid();

        var createdVehicle = await vehicleRepository.AddAsync(vehicle);

        logger.LogInformation("Vehicle created with ID {VehicleId}", createdVehicle.Id);

        return mapper.Map<VehicleResponse>(createdVehicle);
    }

    /// <summary>
    /// Retrieves a vehicle by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle</param>
    /// <returns>The vehicle response if found; otherwise, null</returns>
    public async Task<VehicleResponse?> GetAsync(Guid id)
    {
        logger.LogInformation("Retrieving vehicle with ID {VehicleId}", id);

        var vehicle = await vehicleRepository.GetByIdAsync(id);

        if (vehicle is null)
        {
            logger.LogWarning("Vehicle with ID {VehicleId} not found", id);
            return null;
        }

        return mapper.Map<VehicleResponse>(vehicle);
    }

    /// <summary>
    /// Retrieves all vehicle records from the system
    /// </summary>
    /// <returns>A list of all vehicle responses</returns>
    public async Task<List<VehicleResponse>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all vehicles");

        var vehicles = await vehicleRepository.GetAllAsync();

        logger.LogDebug("Found {Count} vehicles", vehicles.Count);

        return mapper.Map<List<VehicleResponse>>(vehicles);
    }

    /// <summary>
    /// Updates an existing vehicle's information
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to update</param>
    /// <param name="request">Data transfer object containing updated vehicle details</param>
    /// <returns>The updated vehicle response if successful; otherwise, null</returns>
    /// <exception cref="ArgumentException">Thrown when the model generation does not exist</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<VehicleResponse?> UpdateAsync(Guid id, VehicleRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Updating vehicle with ID {VehicleId}", id);

        var existingVehicle = await vehicleRepository.GetByIdAsync(id);
        if (existingVehicle is null)
        {
            logger.LogWarning("Vehicle with ID {VehicleId} not found for update", id);
            return null;
        }

        var _ = await modelGenerationRepository.GetByIdAsync(request.ModelGenerationId)
            ?? throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist.");

        mapper.Map(request, existingVehicle);
        var updatedVehicle = await vehicleRepository.UpdateAsync(existingVehicle);

        logger.LogInformation("Vehicle with ID {VehicleId} updated successfully", id);

        return mapper.Map<VehicleResponse>(updatedVehicle);
    }

    /// <summary>
    /// Deletes a vehicle record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete</param>
    /// <returns>True if deletion was successful; otherwise, false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting vehicle with ID {VehicleId}", id);

        var result = await vehicleRepository.DeleteAsync(id);

        if (result)
        {
            logger.LogInformation("Vehicle with ID {VehicleId} deleted successfully", id);
        }
        else
        {
            logger.LogWarning("Vehicle with ID {VehicleId} not found for deletion", id);
        }

        return result;
    }
}