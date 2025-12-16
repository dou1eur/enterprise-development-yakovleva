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
        ArgumentNullException.ThrowIfNull(request);

        var modelGeneration = await modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);
        if (modelGeneration is not null)
        {
            var vehicle = request.ToDomain();
            var createdVehicle = await vehicleRepository.AddAsync(vehicle);

            return createdVehicle.ToResponse();
        }

        throw new InvalidOperationException($"Model generation with ID {request.ModelGenerationId} does not exist");
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
        ArgumentNullException.ThrowIfNull(request);

        var existingVehicle = await vehicleRepository.GetByIdAsync(id);

        if (existingVehicle is null)
        {
            return null;
        }

        var modelGeneration = await modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);

        if (modelGeneration is not null)
        {
            existingVehicle.LicensePlate = request.LicensePlate;
            existingVehicle.Color = request.Color;
            existingVehicle.GenerationId = request.ModelGenerationId;

            var updatedVehicle = await vehicleRepository.UpdateAsync(existingVehicle);
            return updatedVehicle.ToResponse();
        }

        throw new InvalidOperationException($"Model generation with ID {request.ModelGenerationId} does not exist");
    }

    /// <summary>
    /// Deletes a vehicle record from the system
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await vehicleRepository.DeleteAsync(id);
    }
}