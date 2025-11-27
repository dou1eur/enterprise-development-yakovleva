using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing vehicle generation operations
/// Provides CRUD operations for vehicle generation entities
/// </summary>
public interface IVehicleGenerationService
{
    /// <summary>
    /// Creates a new vehicle generation record
    /// </summary>
    /// <param name="dto">Data transfer object containing generation creation details</param>
    /// <returns>The created vehicle generation data transfer object</returns>
    public VehicleGenerationDto Create(VehicleGenerationCreateUpdateDto dto);

    /// <summary>
    /// Retrieves a vehicle generation by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the generation</param>
    /// <returns>The vehicle generation data transfer object</returns>
    public VehicleGenerationDto Get(Guid id);

    /// <summary>
    /// Retrieves all vehicle generation records
    /// </summary>
    /// <returns>List of all vehicle generation data transfer objects</returns>
    public List<VehicleGenerationDto> GetAll();

    /// <summary>
    /// Updates an existing vehicle generation record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated generation details</param>
    /// <param name="id">The unique identifier of the generation to update</param>
    /// <returns>The updated vehicle generation data transfer object</returns>
    public VehicleGenerationDto Update(VehicleGenerationCreateUpdateDto dto, Guid id);

    /// <summary>
    /// Deletes a vehicle generation record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the generation to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id);

    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The unique identifier of the vehicle model</param>
    /// <returns>List of generation data transfer objects for the specified model</returns>
    public List<VehicleGenerationDto> GetByModelId(Guid modelId);

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generation data transfer objects within the specified year range</returns>
    public List<VehicleGenerationDto> GetByYearRange(int startYear, int endYear);
}