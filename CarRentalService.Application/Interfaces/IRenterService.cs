using CarRentalService.Application.Contracts;
using System;
using System.Collections.Generic;

namespace CarRentalService.Application.Interfaces;

/// <summary>
/// Service interface for managing renter operations
/// Provides methods for renter CRUD operations and business logic
/// </summary>
public interface IRenterService
{
    /// <summary>
    /// Creates a new renter record
    /// </summary>
    /// <param name="dto">Data transfer object containing renter creation details</param>
    /// <returns>The created renter data transfer object</returns>
    public RenterDto Create(RenterCreateUpdateDto dto);

    /// <summary>
    /// Retrieves a renter by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter</param>
    /// <returns>The renter data transfer object</returns>
    public RenterDto Get(Guid id);

    /// <summary>
    /// Retrieves all renter records
    /// </summary>
    /// <returns>List of all renter data transfer objects</returns>
    public List<RenterDto> GetAll();

    /// <summary>
    /// Updates an existing renter record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated renter details</param>
    /// <param name="id">The unique identifier of the renter to update</param>
    /// <returns>The updated renter data transfer object</returns>
    public RenterDto Update(RenterCreateUpdateDto dto, Guid id);

    /// <summary>
    /// Deletes a renter record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id);

    /// <summary>
    /// Retrieves a renter by driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter data transfer object if found, otherwise null</returns>
    public RenterDto? GetByLicenseNumber(string licenseNumber);
}
