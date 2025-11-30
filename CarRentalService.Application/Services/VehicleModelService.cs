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
/// Service for managing vehicle models in the car rental system
/// Handles all CRUD operations for vehicle model entities
/// </summary>
public class VehicleModelService : IVehicleModelService
{
    private readonly IVehicleModelRepository _vehicleModelRepository;

    /// <summary>
    /// Initializes a new instance of VehicleModelService
    /// </summary>
    /// <param name="vehicleModelRepository">The vehicle model repository for data access</param>
    public VehicleModelService(IVehicleModelRepository vehicleModelRepository)
    {
        _vehicleModelRepository = vehicleModelRepository;
    }

    /// <summary>
    /// Creates a new vehicle model record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing vehicle model creation details</param>
    /// <returns>The created vehicle model data transfer object</returns>
    public async Task<VehicleModelDto> CreateAsync(CreateVehicleModelRequest request)
    {
        var vehicleModel = request.ToDomain();
        var createdVehicleModel = await _vehicleModelRepository.AddAsync(vehicleModel);
        return createdVehicleModel.ToDto();
    }

    /// <summary>
    /// Retrieves a specific vehicle model by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model</param>
    /// <returns>The vehicle model data transfer object if found; otherwise, null</returns>
    public async Task<VehicleModelDto?> GetAsync(Guid id)
    {
        var vehicleModel = await _vehicleModelRepository.GetByIdAsync(id);
        return vehicleModel?.ToDto();
    }

    /// <summary>
    /// Retrieves all vehicle model records from the system
    /// </summary>
    /// <returns>List of all vehicle model data transfer objects</returns>
    public async Task<List<VehicleModelDto>> GetAllAsync()
    {
        var vehicleModels = await _vehicleModelRepository.GetAllAsync();
        return vehicleModels.ConvertAll(vm => vm.ToDto());
    }

    /// <summary>
    /// Updates an existing vehicle model's information
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to update</param>
    /// <param name="request">Data transfer object containing updated vehicle model details</param>
    /// <returns>The updated vehicle model data transfer object if successful; otherwise, null</returns>
    public async Task<VehicleModelDto?> UpdateAsync(Guid id, UpdateVehicleModelRequest request)
    {
        var existingVehicleModel = await _vehicleModelRepository.GetByIdAsync(id);
        if (existingVehicleModel == null)
            return null;

        existingVehicleModel.Name = request.Name;
        existingVehicleModel.DriveType = request.DriveType;
        existingVehicleModel.SeatCount = request.SeatCount;
        existingVehicleModel.BodyType = request.BodyType;
        existingVehicleModel.VehicleClass = request.VehicleClass;

        var updatedVehicleModel = await _vehicleModelRepository.UpdateAsync(existingVehicleModel);
        return updatedVehicleModel.ToDto();
    }

    /// <summary>
    /// Deletes a vehicle model record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _vehicleModelRepository.DeleteAsync(id);
    }
}