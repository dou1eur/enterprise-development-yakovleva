using CarRentalService.Application.Contracts.Renter;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing renter operations
/// Provides methods for Create, Read, Update, and Delete (CRUD) operations on renters
/// </summary>
public interface IRenterService
{
    /// <summary>
    /// Creates a new renter record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing renter creation details</param>
    /// <returns>The created renter response</returns>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public Task<RenterResponse> CreateAsync(RenterRequest request);

    /// <summary>
    /// Retrieves a renter by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter</param>
    /// <returns>The renter response if found; otherwise, <c>null</c></returns>
    public Task<RenterResponse?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all renter records from the system
    /// </summary>
    /// <returns>A list of all renter responses.</returns>
    public Task<List<RenterResponse>> GetAllAsync();

    /// <summary>
    /// Updates an existing renter's information
    /// </summary>
    /// <param name="id">The unique identifier of the renter to update</param>
    /// <param name="request">Data transfer object containing updated renter details</param>
    /// <returns>The updated renter response if successful; otherwise, <c>null</c></returns>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public Task<RenterResponse?> UpdateAsync(Guid id, RenterRequest request);

    /// <summary>
    /// Deletes a renter record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the renter to delete</param>
    /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c></returns>
    public Task<bool> DeleteAsync(Guid id);
}