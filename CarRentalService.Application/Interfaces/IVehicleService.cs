using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing vehicle operations
/// Provides CRUD operations and analytical queries for vehicle entities
/// </summary>
public interface IVehicleService
{
    /// <summary>
    /// Creates a new vehicle record
    /// </summary>
    /// <param name="dto">Data transfer object containing vehicle creation details</param>
    /// <returns>The created vehicle data transfer object</returns>
    public VehicleDto Create(VehicleCreateUpdateDto dto);

    /// <summary>
    /// Retrieves a vehicle by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle</param>
    /// <returns>The vehicle data transfer object</returns>
    public VehicleDto Get(Guid id);

    /// <summary>
    /// Retrieves all vehicle records
    /// </summary>
    /// <returns>List of all vehicle data transfer objects</returns>
    public List<VehicleDto> GetAll();

    /// <summary>
    /// Updates an existing vehicle record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated vehicle details</param>
    /// <param name="id">The unique identifier of the vehicle to update</param>
    /// <returns>The updated vehicle data transfer object</returns>
    public VehicleDto Update(VehicleCreateUpdateDto dto, Guid id);

    /// <summary>
    /// Deletes a vehicle record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id);

    /// <summary>
    /// Retrieves a vehicle by its license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle data transfer object</returns>
    public VehicleDto? GetByLicensePlate(string licensePlate);

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The unique identifier of the vehicle model</param>
    /// <returns>List of vehicle data transfer objects for the specified model</returns>
    public List<VehicleDto> GetByModel(Guid modelId);
}