using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Interfaces;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Application.Mappings;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing vehicles in the car rental system
/// Handles vehicle operations with validation of related entities
/// </summary>
public class VehicleService(IVehicleRepository vehicleRepository, IModelGenerationRepository modelGenerationRepository) : IVehicleService
{
    /// <summary>
    /// Creates a new vehicle record in the system
    /// </summary>
    public async Task<VehicleResponse> CreateAsync(VehicleRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var modelGeneration = await modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist", nameof(request.ModelGenerationId));

        var vehicle = request.ToDomain();
        var createdVehicle = await vehicleRepository.AddAsync(vehicle);

        return createdVehicle.ToResponse();
    }

    /// <summary>
    /// Retrieves a vehicle by its unique identifier
    /// </summary>
    public async Task<VehicleResponse?> GetAsync(Guid id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(id);
        return vehicle?.ToResponse();
    }

    /// <summary>
    /// Retrieves all vehicle records from the system
    /// </summary>
    public async Task<List<VehicleResponse>> GetAllAsync()
    {
        var vehicles = await vehicleRepository.GetAllAsync();
        return vehicles.ToResponseList();
    }

    /// <summary>
    /// Updates an existing vehicle's information
    /// </summary>
    public async Task<VehicleResponse?> UpdateAsync(Guid id, VehicleRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var existingVehicle = await vehicleRepository.GetByIdAsync(id);
        if (existingVehicle == null)
            return null;

        var modelGeneration = await modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist", nameof(request.ModelGenerationId));

        existingVehicle.LicensePlate = request.LicensePlate;
        existingVehicle.Color = request.Color;
        existingVehicle.GenerationId = request.ModelGenerationId;

        var updatedVehicle = await vehicleRepository.UpdateAsync(existingVehicle);
        return updatedVehicle.ToResponse();
    }

    /// <summary>
    /// Deletes a vehicle record from the system
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await vehicleRepository.DeleteAsync(id);
    }
}