using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Application.Mappings;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing vehicles in the car rental system
/// Handles vehicle operations with validation of related entities
/// </summary>
public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IModelGenerationRepository _modelGenerationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleService"/> class
    /// </summary>
    public VehicleService(
        IVehicleRepository vehicleRepository,
        IModelGenerationRepository modelGenerationRepository)
    {
        _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        _modelGenerationRepository = modelGenerationRepository ?? throw new ArgumentNullException(nameof(modelGenerationRepository));
    }

    /// <summary>
    /// Creates a new vehicle record in the system
    /// </summary>
    public async Task<VehicleResponse> CreateAsync(VehicleRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist", nameof(request.ModelGenerationId));

        var vehicle = request.ToDomain();
        var createdVehicle = await _vehicleRepository.AddAsync(vehicle);

        return createdVehicle.ToResponse();
    }

    /// <summary>
    /// Retrieves a vehicle by its unique identifier
    /// </summary>
    public async Task<VehicleResponse?> GetAsync(Guid id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        return vehicle?.ToResponse();
    }

    /// <summary>
    /// Retrieves all vehicle records from the system
    /// </summary>
    public async Task<List<VehicleResponse>> GetAllAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return vehicles.ToResponseList();
    }

    /// <summary>
    /// Updates an existing vehicle's information
    /// </summary>
    public async Task<VehicleResponse?> UpdateAsync(Guid id, VehicleRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var existingVehicle = await _vehicleRepository.GetByIdAsync(id);
        if (existingVehicle == null)
            return null;

        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist", nameof(request.ModelGenerationId));

        existingVehicle.LicensePlate = request.LicensePlate;
        existingVehicle.Color = request.Color;
        existingVehicle.GenerationId = request.ModelGenerationId;

        var updatedVehicle = await _vehicleRepository.UpdateAsync(existingVehicle);
        return updatedVehicle.ToResponse();
    }

    /// <summary>
    /// Deletes a vehicle record from the system
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _vehicleRepository.DeleteAsync(id);
    }
}