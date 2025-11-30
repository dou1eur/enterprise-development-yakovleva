using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Application.Mappings;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing renters in the car rental system
/// Handles all CRUD operations for renter entities
/// </summary>
public class RenterService : IRenterService
{
    private readonly IRenterRepository _renterRepository;

    /// <summary>
    /// Initializes a new instance of RenterService
    /// </summary>
    /// <param name="renterRepository">The renter repository for data access</param>
    public RenterService(IRenterRepository renterRepository)
    {
        _renterRepository = renterRepository;
    }

    /// <summary>
    /// Creates a new renter record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing renter creation details</param>
    /// <returns>The created renter data transfer object</returns>
    public async Task<RenterDto> CreateAsync(CreateRenterRequest request)
    {
        var renter = request.ToDomain();
        var createdRenter = await _renterRepository.AddAsync(renter);
        return createdRenter.ToDto();
    }

    /// <summary>
    /// Retrieves a specific renter by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter</param>
    /// <returns>The renter data transfer object if found; otherwise, null</returns>
    public async Task<RenterDto?> GetAsync(Guid id)
    {
        var renter = await _renterRepository.GetByIdAsync(id);
        return renter?.ToDto();
    }

    /// <summary>
    /// Retrieves all renter records from the system
    /// </summary>
    /// <returns>List of all renter data transfer objects</returns>
    public async Task<List<RenterDto>> GetAllAsync()
    {
        var renters = await _renterRepository.GetAllAsync();
        return renters.ConvertAll(r => r.ToDto());
    }

    /// <summary>
    /// Updates an existing renter's information
    /// </summary>
    /// <param name="id">The unique identifier of the renter to update</param>
    /// <param name="request">Data transfer object containing updated renter details</param>
    /// <returns>The updated renter data transfer object if successful; otherwise, null</returns>
    public async Task<RenterDto?> UpdateAsync(Guid id, UpdateRenterRequest request)
    {
        var existingRenter = await _renterRepository.GetByIdAsync(id);
        if (existingRenter == null)
            return null;

        existingRenter.LicenseNumber = request.LicenseNumber;
        existingRenter.FullName = request.FullName;
        existingRenter.DateOfBirth = request.DateOfBirth;

        var updatedRenter = await _renterRepository.UpdateAsync(existingRenter);
        return updatedRenter.ToDto();
    }

    /// <summary>
    /// Deletes a renter record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the renter to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _renterRepository.DeleteAsync(id);
    }
}