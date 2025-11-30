using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing rental operations
/// Provides methods for basic CRUD operations on rentals
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Creates a new rental record
    /// </summary>
    /// <param name="request">Data transfer object containing rental creation details</param>
    /// <returns>The created rental data transfer object</returns>
    public Task<RentalDto> CreateAsync(CreateRentalRequest request);

    /// <summary>
    /// Retrieves a rental by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental</param>
    /// <returns>The rental data transfer object if found; otherwise, null</returns>
    public Task<RentalDto?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all rental records
    /// </summary>
    /// <returns>List of all rental data transfer objects</returns>
    public Task<List<RentalDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing rental record
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update</param>
    /// <param name="request">Data transfer object containing updated rental details</param>
    /// <returns>The updated rental data transfer object if successful; otherwise, null</returns>
    public Task<RentalDto?> UpdateAsync(Guid id, UpdateRentalRequest request);

    /// <summary>
    /// Deletes a rental record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public Task<bool> DeleteAsync(Guid id);
}