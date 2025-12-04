using CarRentalService.Application.Contracts;
using CarRentalService.Application.Contracts.VehicleModel;
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
    /// Initializes a new instance of the <see cref="VehicleModelService"/> class
    /// </summary>
    /// <param name="vehicleModelRepository">The vehicle model repository for data access</param>
    /// <exception cref="ArgumentNullException">Thrown when the repository is null</exception>
    public VehicleModelService(IVehicleModelRepository vehicleModelRepository)
    {
        _vehicleModelRepository = vehicleModelRepository ?? throw new ArgumentNullException(nameof(vehicleModelRepository));
    }

    /// <summary>
    /// Creates a new vehicle model record in the system
    /// </summary>
    public async Task<VehicleModelResponse> CreateAsync(VehicleModelRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var vehicleModel = request.ToDomain();
        var createdVehicleModel = await _vehicleModelRepository.AddAsync(vehicleModel);

        return createdVehicleModel.ToResponse();
    }

    /// <summary>
    /// Retrieves a vehicle model by its unique identifier
    /// </summary>
    public async Task<VehicleModelResponse?> GetAsync(Guid id)
    {
        var vehicleModel = await _vehicleModelRepository.GetByIdAsync(id);
        return vehicleModel?.ToResponse();
    }

    /// <summary>
    /// Retrieves all vehicle model records from the system
    /// </summary>
    public async Task<List<VehicleModelResponse>> GetAllAsync()
    {
        var vehicleModels = await _vehicleModelRepository.GetAllAsync();
        return vehicleModels.ToResponseList();
    }

    /// <summary>
    /// Updates an existing vehicle model's information
    /// </summary>
    public async Task<VehicleModelResponse?> UpdateAsync(Guid id, VehicleModelRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var existingVehicleModel = await _vehicleModelRepository.GetByIdAsync(id);
        if (existingVehicleModel == null)
            return null;

        existingVehicleModel.Name = request.Name;
        existingVehicleModel.DriveType = request.DriveType;
        existingVehicleModel.SeatCount = request.SeatCount;
        existingVehicleModel.BodyType = request.BodyType;
        existingVehicleModel.VehicleClass = request.VehicleClass;

        var updatedVehicleModel = await _vehicleModelRepository.UpdateAsync(existingVehicleModel);
        return updatedVehicleModel.ToResponse();
    }

    /// <summary>
    /// Deletes a vehicle model record from the system
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _vehicleModelRepository.DeleteAsync(id);
    }
}