using CarRentalService.Application.Contracts;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing vehicle model operations
/// Provides methods for basic CRUD operations on vehicle models
/// </summary>
public interface IVehicleModelService
{
    /// <summary>
    /// Creates a new vehicle model record
    /// </summary>
    /// <param name="request">Data transfer object containing vehicle model creation details</param>
    /// <returns>The created vehicle model data transfer object</returns>
    public Task<VehicleModelDto> CreateAsync(CreateVehicleModelRequest request);

    /// <summary>
    /// Retrieves a vehicle model by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model</param>
    /// <returns>The vehicle model data transfer object if found; otherwise, null</returns>
    public Task<VehicleModelDto?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all vehicle model records
    /// </summary>
    /// <returns>List of all vehicle model data transfer objects</returns>
    public Task<List<VehicleModelDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing vehicle model record
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to update</param>
    /// <param name="request">Data transfer object containing updated vehicle model details</param>
    /// <returns>The updated vehicle model data transfer object if successful; otherwise, null</returns>
    public Task<VehicleModelDto?> UpdateAsync(Guid id, UpdateVehicleModelRequest request);

    /// <summary>
    /// Deletes a vehicle model record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle model to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public Task<bool> DeleteAsync(Guid id);
}