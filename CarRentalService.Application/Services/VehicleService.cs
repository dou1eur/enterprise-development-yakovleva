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
/// Service for managing vehicles in the car rental system
/// Handles vehicle operations with validation of related entities
/// </summary>
public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IModelGenerationRepository _modelGenerationRepository;

    /// <summary>
    /// Initializes a new instance of VehicleService
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository for data access</param>
    /// <param name="modelGenerationRepository">The model generation repository for validation</param>
    public VehicleService(
        IVehicleRepository vehicleRepository,
        IModelGenerationRepository modelGenerationRepository)
    {
        _vehicleRepository = vehicleRepository;
        _modelGenerationRepository = modelGenerationRepository;
    }

    /// <summary>
    /// Creates a new vehicle record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing vehicle creation details</param>
    /// <returns>The created vehicle data transfer object</returns>
    /// <exception cref="ArgumentException">Thrown when the specified model generation does not exist</exception>
    public async Task<VehicleDto> CreateAsync(CreateVehicleRequest request)
    {
        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist");

        var vehicle = request.ToDomain();
        var createdVehicle = await _vehicleRepository.AddAsync(vehicle);
        return createdVehicle.ToDto();
    }

    /// <summary>
    /// Retrieves a specific vehicle by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle</param>
    /// <returns>The vehicle data transfer object if found; otherwise, null</returns>
    public async Task<VehicleDto?> GetAsync(Guid id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        return vehicle?.ToDto();
    }

    /// <summary>
    /// Retrieves all vehicle records from the system
    /// </summary>
    /// <returns>List of all vehicle data transfer objects</returns>
    public async Task<List<VehicleDto>> GetAllAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return vehicles.ConvertAll(v => v.ToDto());
    }

    /// <summary>
    /// Updates an existing vehicle's information
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to update</param>
    /// <param name="request">Data transfer object containing updated vehicle details</param>
    /// <returns>The updated vehicle data transfer object if successful; otherwise, null</returns>
    /// <exception cref="ArgumentException">Thrown when the specified model generation does not exist</exception>
    public async Task<VehicleDto?> UpdateAsync(Guid id, UpdateVehicleRequest request)
    {
        var existingVehicle = await _vehicleRepository.GetByIdAsync(id);
        if (existingVehicle == null)
            return null;

        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(request.ModelGenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation with ID {request.ModelGenerationId} does not exist");

        existingVehicle.LicensePlate = request.LicensePlate;
        existingVehicle.Color = request.Color;
        existingVehicle.GenerationId = request.ModelGenerationId;

        var updatedVehicle = await _vehicleRepository.UpdateAsync(existingVehicle);
        return updatedVehicle.ToDto();
    }

    /// <summary>
    /// Deletes a vehicle record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _vehicleRepository.DeleteAsync(id);
    }
}