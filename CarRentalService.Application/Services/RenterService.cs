using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service implementation for managing renter operations
/// Handles business logic for renter CRUD operations
/// </summary>
public class RenterService : IRenterService
{
    private readonly IRenterRepository _repository;

    /// <summary>
    /// Initializes a new instance of the RenterService class
    /// </summary>
    /// <param name="repository">The renter repository</param>
    public RenterService(IRenterRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Creates a new renter record
    /// </summary>
    /// <param name="dto">Data transfer object containing renter creation details</param>
    /// <returns>The created renter data transfer object</returns>
    public RenterDto Create(RenterCreateUpdateDto dto)
    {
        var renter = new Renter
        {
            LicenseNumber = dto.LicenseNumber,
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth
        };

        _repository.AddAsync(renter);
        return MapToDto(renter);
    }

    /// <summary>
    /// Retrieves a renter by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter</param>
    /// <returns>The renter data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when renter with specified ID is not found</exception>
    public RenterDto Get(Guid id)
    {
        var renter = _repository.GetByIdAsync(id).Result;
        if (renter == null)
            throw new KeyNotFoundException($"Renter with ID {id} not found");
        return MapToDto(renter);
    }

    /// <summary>
    /// Retrieves all renter records
    /// </summary>
    /// <returns>List of all renter data transfer objects</returns>
    public List<RenterDto> GetAll()
    {
        var renters = _repository.GetAllAsync().Result;
        return renters.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Updates an existing renter record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated renter details</param>
    /// <param name="id">The unique identifier of the renter to update</param>
    /// <returns>The updated renter data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when renter with specified ID is not found</exception>
    public RenterDto Update(RenterCreateUpdateDto dto, Guid id)
    {
        var existingRenter = _repository.GetByIdAsync(id).Result;
        if (existingRenter == null)
            throw new KeyNotFoundException($"Renter with ID {id} not found");

        existingRenter.LicenseNumber = dto.LicenseNumber;
        existingRenter.FullName = dto.FullName;
        existingRenter.DateOfBirth = dto.DateOfBirth;

        _repository.UpdateAsync(existingRenter);
        return MapToDto(existingRenter);
    }

    /// <summary>
    /// Deletes a renter record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id)
    {
        return _repository.DeleteAsync(id).Result;
    }

    /// <summary>
    /// Retrieves a renter by driver's license number
    /// </summary>
    /// <param name="licenseNumber">The driver's license number</param>
    /// <returns>The renter data transfer object if found, otherwise null</returns>
    public RenterDto? GetByLicenseNumber(string licenseNumber)
    {
        var renter = _repository.GetByLicenseNumberAsync(licenseNumber).Result;
        return renter != null ? MapToDto(renter) : null;
    }

    /// <summary>
    /// Maps a Renter domain entity to a RenterDto data transfer object
    /// </summary>
    /// <param name="renter">The renter domain entity</param>
    /// <returns>The mapped renter data transfer object</returns>
    private static RenterDto MapToDto(Renter renter)
    {
        return new RenterDto
        {
            Id = renter.Id,
            LicenseNumber = renter.LicenseNumber,
            FullName = renter.FullName,
            DateOfBirth = renter.DateOfBirth
        };
    }
}