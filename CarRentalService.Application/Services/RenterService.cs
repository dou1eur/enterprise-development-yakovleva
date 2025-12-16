using CarRentalService.Application.Contracts.Renter;
using CarRentalService.Application.Interfaces;
using CarRentalService.Interfaces.Repositories;
using CarRentalService.Application.Mappings;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing renters in the car rental system
/// Handles all CRUD operations for renter entities
/// </summary>
public class RenterService(IRenterRepository renterRepository): IRenterService
{
    /// <summary>
    /// Creates a new renter record in the system
    /// </summary>
    /// <param name="request">Data transfer object containing renter creation details</param>
    /// <returns>The created renter response.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<RenterResponse> CreateAsync(RenterRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.DateOfBirth > DateTime.UtcNow.AddYears(-18))
            throw new InvalidOperationException("Renter must be at least 18 years old.");

        var renter = request.ToDomain();
        var createdRenter = await renterRepository.AddAsync(renter);

        return createdRenter.ToResponse();
    }

    /// <summary>
    /// Retrieves a renter by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the renter</param>
    /// <returns>The renter response if found; otherwise, <c>null</c></returns>
    public async Task<RenterResponse?> GetAsync(Guid id)
    {
        var renter = await renterRepository.GetByIdAsync(id);
        return renter?.ToResponse();
    }

    /// <summary>
    /// Retrieves all renter records from the system
    /// </summary>
    /// <returns>A list of all renter responses</returns>
    public async Task<List<RenterResponse>> GetAllAsync()
    {
        var renters = await renterRepository.GetAllAsync();
        return renters.ToResponseList();
    }

    /// <summary>
    /// Updates an existing renter's information
    /// </summary>
    /// <param name="id">The unique identifier of the renter to update</param>
    /// <param name="request">Data transfer object containing updated renter details</param>
    /// <returns>The updated renter response if successful; otherwise, <c>null</c></returns>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<RenterResponse?> UpdateAsync(Guid id, RenterRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var existingRenter = await renterRepository.GetByIdAsync(id);
        if (existingRenter is null)
            return null;

        if (request.DateOfBirth > DateTime.UtcNow.AddYears(-18))
            throw new InvalidOperationException("Renter must be at least 18 years old.");

        existingRenter.LicenseNumber = request.LicenseNumber;
        existingRenter.FullName = request.FullName;
        existingRenter.DateOfBirth = request.DateOfBirth;

        var updatedRenter = await renterRepository.UpdateAsync(existingRenter);
        return updatedRenter.ToResponse();
    }

    /// <summary>
    /// Deletes a renter record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the renter to delete</param>
    /// <returns><c>true</c> if deletion was successful; otherwise, <c>false</c></returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await renterRepository.DeleteAsync(id);
    }
}