using CarRentalService.Application.Contracts;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing vehicle model operations
/// Provides CRUD operations for vehicle model entities
/// </summary>
public interface IVehicleModelService
{
    /// <summary>
    /// Creates a new vehicle model record
    /// </summary>
    /// <param name="dto">Data transfer object containing model creation details</param>
    /// <returns>The created vehicle model data transfer object</returns>
    public VehicleModelDto Create(VehicleModelCreateUpdateDto dto);

    /// <summary>
    /// Retrieves a vehicle model by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model</param>
    /// <returns>The vehicle model data transfer object</returns>
    public VehicleModelDto Get(Guid id);

    /// <summary>
    /// Retrieves all vehicle model records
    /// </summary>
    /// <returns>List of all vehicle model data transfer objects</returns>
    public List<VehicleModelDto> GetAll();

    /// <summary>
    /// Updates an existing vehicle model record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated model details</param>
    /// <param name="id">The unique identifier of the model to update</param>
    /// <returns>The updated vehicle model data transfer object</returns>
    public VehicleModelDto Update(VehicleModelCreateUpdateDto dto, Guid id);

    /// <summary>
    /// Deletes a vehicle model record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id);

    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model data transfer object if found, otherwise null</returns>
    public VehicleModelDto? GetByName(string name);

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle model data transfer objects with the specified body type</returns>
    public List<VehicleModelDto> GetByBodyType(BodyType bodyType);

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle model data transfer objects with the specified vehicle class</returns>
    public List<VehicleModelDto> GetByVehicleClass(VehicleClass vehicleClass);
}