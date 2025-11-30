using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

public interface IVehicleService
{
    /// <summary>
    /// Creates a new vehicle record
    /// </summary>
    /// <param name="request">Data transfer object containing vehicle creation details</param>
    /// <returns>The created vehicle data transfer object</returns>
    public Task<VehicleDto> CreateAsync(CreateVehicleRequest request);

    /// <summary>
    /// Retrieves a vehicle by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle</param>
    /// <returns>The vehicle data transfer object if found; otherwise, null</returns>
    public Task<VehicleDto?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all vehicle records
    /// </summary>
    /// <returns>List of all vehicle data transfer objects</returns>
    public Task<List<VehicleDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing vehicle record
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to update</param>
    /// <param name="request">Data transfer object containing updated vehicle details</param>
    /// <returns>The updated vehicle data transfer object if successful; otherwise, null</returns>
    public Task<VehicleDto?> UpdateAsync(Guid id, UpdateVehicleRequest request);

    /// <summary>
    /// Deletes a vehicle record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public Task<bool> DeleteAsync(Guid id);
}