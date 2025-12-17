using AutoMapper;
using CarRentalService.Application.Contracts.Renter;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using CarRentalService.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing renters in the car rental system
/// Handles all CRUD operations for renter entities
/// </summary>
/// <param name="renterRepository">Repository for renters</param>
/// <param name="mapper">AutoMapper instance</param>
/// <param name="logger">Logger instance</param>
public class RenterService(
    IRenterRepository renterRepository,
    IMapper mapper,
    ILogger<RenterService> logger) : IRenterService
{
    /// <summary>
    /// Creates a new renter record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing renter creation details</param>
    /// <returns>The created renter response</returns>
    /// <exception cref="ArgumentException">Thrown when the renter is under 18 years old</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<RenterResponse> CreateAsync(RenterRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Creating new renter with license number {LicenseNumber}", request.LicenseNumber);

        ValidateAge(request.DateOfBirth);

        var renter = mapper.Map<Renter>(request);
        renter.Id = Guid.NewGuid();

        var createdRenter = await renterRepository.AddAsync(renter);

        logger.LogInformation("Renter created with ID {RenterId}", createdRenter.Id);

        return mapper.Map<RenterResponse>(createdRenter);
    }

    /// <summary>
    /// Retrieves a renter by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter</param>
    /// <returns>The renter response if found; otherwise, null</returns>
    public async Task<RenterResponse?> GetAsync(Guid id)
    {
        logger.LogInformation("Retrieving renter with ID {RenterId}", id);

        var renter = await renterRepository.GetByIdAsync(id);

        if (renter is null)
        {
            logger.LogWarning("Renter with ID {RenterId} not found", id);
            return null;
        }

        return mapper.Map<RenterResponse>(renter);
    }

    /// <summary>
    /// Retrieves all renter records from the system
    /// </summary>
    /// <returns>A list of all renter responses</returns>
    public async Task<List<RenterResponse>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all renters");

        var renters = await renterRepository.GetAllAsync();

        logger.LogDebug("Found {Count} renters", renters.Count);

        return mapper.Map<List<RenterResponse>>(renters);
    }

    /// <summary>
    /// Updates an existing renter's information
    /// </summary>
    /// <param name="id">The unique identifier of the renter to update</param>
    /// <param name="request">Data transfer object containing updated renter details</param>
    /// <returns>The updated renter response if successful; otherwise, null</returns>
    /// <exception cref="ArgumentException">Thrown when the renter is under 18 years old</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<RenterResponse?> UpdateAsync(Guid id, RenterRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Updating renter with ID {RenterId}", id);

        var existingRenter = await renterRepository.GetByIdAsync(id);
        if (existingRenter is null)
        {
            logger.LogWarning("Renter with ID {RenterId} not found for update", id);
            return null;
        }

        ValidateAge(request.DateOfBirth);

        mapper.Map(request, existingRenter);
        var updatedRenter = await renterRepository.UpdateAsync(existingRenter);

        logger.LogInformation("Renter with ID {RenterId} updated successfully", id);

        return mapper.Map<RenterResponse>(updatedRenter);
    }

    /// <summary>
    /// Deletes a renter record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the renter to delete</param>
    /// <returns>True if deletion was successful; otherwise, false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting renter with ID {RenterId}", id);

        var result = await renterRepository.DeleteAsync(id);

        if (result)
        {
            logger.LogInformation("Renter with ID {RenterId} deleted successfully", id);
        }
        else
        {
            logger.LogWarning("Renter with ID {RenterId} not found for deletion", id);
        }

        return result;
    }

    /// <summary>
    /// Validates that the renter is at least 18 years old
    /// </summary>
    /// <param name="dateOfBirth">The date of birth to validate</param>
    /// <exception cref="ArgumentException">Thrown when the renter is under 18 years old</exception>
    private static void ValidateAge(DateTime dateOfBirth)
    {
        if (dateOfBirth.AddYears(18) > DateTime.UtcNow)
        {
            throw new ArgumentException("Renter must be at least 18.");
        }
    }
}