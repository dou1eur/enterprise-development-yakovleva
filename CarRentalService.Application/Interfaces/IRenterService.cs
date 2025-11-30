using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing renter operations
/// Provides methods for basic CRUD operations on renters
/// </summary>
public interface IRenterService
{
    /// <summary>
    /// Creates a new renter record
    /// </summary>
    /// <param name="request">Data transfer object containing renter creation details</param>
    /// <returns>The created renter data transfer object</returns>
    public Task<RenterDto> CreateAsync(CreateRenterRequest request);

    /// <summary>
    /// Retrieves a renter by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter</param>
    /// <returns>The renter data transfer object if found; otherwise, null</returns>
    public Task<RenterDto?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all renter records
    /// </summary>
    /// <returns>List of all renter data transfer objects</returns>
    public Task<List<RenterDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing renter record
    /// </summary>
    /// <param name="id">The unique identifier of the renter to update</param>
    /// <param name="request">Data transfer object containing updated renter details</param>
    /// <returns>The updated renter data transfer object if successful; otherwise, null</returns>
    public Task<RenterDto?> UpdateAsync(Guid id, UpdateRenterRequest request);

    /// <summary>
    /// Deletes a renter record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public Task<bool> DeleteAsync(Guid id);
}