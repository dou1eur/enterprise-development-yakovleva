using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing rental operations.
/// Provides CRUD operations and analytical queries for rental entities
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Creates a new rental record
    /// </summary>
    /// <param name="dto">Data transfer object containing rental creation details</param>
    /// <returns>The created rental data transfer object</returns>
    public RentalDto Create(RentalCreateUpdateDto dto);

    /// <summary>
    /// Retrieves a rental by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental</param>
    /// <returns>The rental data transfer object</returns>
    public RentalDto Get(Guid id);

    /// <summary>
    /// Retrieves all rental records
    /// </summary>
    /// <returns>List of all rental data transfer objects</returns>
    public List<RentalDto> GetAll();

    /// <summary>
    /// Updates an existing rental record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated rental details</param>
    /// <param name="id">The unique identifier of the rental to update</param>
    /// <returns>The updated rental data transfer object</returns>
    public RentalDto Update(RentalCreateUpdateDto dto, Guid id);

    /// <summary>
    /// Deletes a rental record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id);

    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The unique identifier of the renter</param>
    /// <returns>List of rental data ransfer objects for the specified renter</returns>
    public List<RentalDto> GetByRenterId(Guid renterId);

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <returns>List of rental data transfer objects for the specified vehicle</returns>
    public List<RentalDto> GetByVehicleId(Guid vehicleId);

    /// <summary>
    /// Calculates the rental cost for a vehicle and duration
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <param name="durationHours">The rental duration in hours</param>
    /// <returns>The calculated rental cost</returns>
    public decimal CalculateRentalCost(Guid vehicleId, int durationHours);
}