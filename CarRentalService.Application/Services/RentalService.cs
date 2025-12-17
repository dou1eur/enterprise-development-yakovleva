using AutoMapper;
using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Application.Interfaces;
using CarRentalService.Domain;
using CarRentalService.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service for managing rentals in the car rental system
/// Handles rental operations including validation and cost calculation
/// </summary>
/// <param name="rentalRepository">Repository for rentals</param>
/// <param name="renterRepository">Repository for renters</param>
/// <param name="vehicleRepository">Repository for vehicles</param>
/// <param name="modelGenerationRepository">Repository for model generations</param>
/// <param name="mapper">AutoMapper instance</param>
/// <param name="logger">Logger instance</param>
public class RentalService(
    IRentalRepository rentalRepository,
    IRenterRepository renterRepository,
    IVehicleRepository vehicleRepository,
    IModelGenerationRepository modelGenerationRepository,
    IMapper mapper,
    ILogger<RentalService> logger) : IRentalService
{
    /// <summary>
    /// Creates a new rental record with validation and cost calculation
    /// </summary>
    /// <param name="request">Data transfer object containing rental creation details</param>
    /// <returns>The created rental response</returns>
    /// <exception cref="ArgumentException">Thrown when related entities are not found</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<RentalResponse> CreateAsync(RentalRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation(
            "Creating new rental for vehicle {VehicleId} by renter {RenterId}",
            request.VehicleId, request.CustomerId);

        var _ = await renterRepository.GetByIdAsync(request.CustomerId)
            ?? throw new ArgumentException($"Renter with ID {request.CustomerId} does not exist.");

        var vehicle = await vehicleRepository.GetByIdAsync(request.VehicleId)
            ?? throw new ArgumentException($"Vehicle with ID {request.VehicleId} does not exist.");

        var modelGeneration = await modelGenerationRepository.GetByIdAsync(vehicle.GenerationId)
            ?? throw new ArgumentException($"Model generation for vehicle {request.VehicleId} does not exist.");

        var rental = mapper.Map<Rental>(request);
        rental.Id = Guid.NewGuid();
        rental.TotalCost = modelGeneration.RentalPricePerHour * rental.DurationHours;

        var createdRental = await rentalRepository.AddAsync(rental);

        logger.LogInformation("Rental created with ID {RentalId}", createdRental.Id);

        return mapper.Map<RentalResponse>(createdRental);
    }

    /// <summary>
    /// Retrieves a rental by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental</param>
    /// <returns>The rental response if found; otherwise, null</returns>
    public async Task<RentalResponse?> GetAsync(Guid id)
    {
        logger.LogInformation("Retrieving rental with ID {RentalId}", id);

        var rental = await rentalRepository.GetByIdAsync(id);

        if (rental is null)
        {
            logger.LogWarning("Rental with ID {RentalId} not found", id);
            return null;
        }

        return mapper.Map<RentalResponse>(rental);
    }

    /// <summary>
    /// Retrieves all rental records from the system
    /// </summary>
    /// <returns>A list of all rental responses</returns>
    public async Task<List<RentalResponse>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all rentals");

        var rentals = await rentalRepository.GetAllAsync();

        logger.LogDebug("Found {Count} rentals", rentals.Count);

        return mapper.Map<List<RentalResponse>>(rentals);
    }

    /// <summary>
    /// Updates an existing rental with validation and cost recalculation
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update</param>
    /// <param name="request">Data transfer object containing updated rental details</param>
    /// <returns>The updated rental response if successful; otherwise, null</returns>
    /// <exception cref="ArgumentException">Thrown when related entities are not found</exception>
    /// <exception cref="ArgumentNullException">Thrown when the request is null</exception>
    public async Task<RentalResponse?> UpdateAsync(Guid id, RentalRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        logger.LogInformation("Updating rental with ID {RentalId}", id);

        var existingRental = await rentalRepository.GetByIdAsync(id);
        if (existingRental is null)
        {
            logger.LogWarning("Rental with ID {RentalId} not found for update", id);
            return null;
        }

        var _ = await renterRepository.GetByIdAsync(request.CustomerId)
            ?? throw new ArgumentException($"Renter with ID {request.CustomerId} does not exist.");

        var vehicle = await vehicleRepository.GetByIdAsync(request.VehicleId)
            ?? throw new ArgumentException($"Vehicle with ID {request.VehicleId} does not exist.");

        var modelGeneration = await modelGenerationRepository.GetByIdAsync(vehicle.GenerationId)
            ?? throw new ArgumentException($"Model generation for vehicle {request.VehicleId} does not exist.");

        mapper.Map(request, existingRental);
        existingRental.TotalCost = modelGeneration.RentalPricePerHour * existingRental.DurationHours;

        var updatedRental = await rentalRepository.UpdateAsync(existingRental);

        logger.LogInformation("Rental with ID {RentalId} updated successfully", id);

        return mapper.Map<RentalResponse>(updatedRental);
    }

    /// <summary>
    /// Deletes a rental record from the system
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete</param>
    /// <returns>True if deletion was successful; otherwise, false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting rental with ID {RentalId}", id);

        var result = await rentalRepository.DeleteAsync(id);

        if (result)
        {
            logger.LogInformation("Rental with ID {RentalId} deleted successfully", id);
        }
        else
        {
            logger.LogWarning("Rental with ID {RentalId} not found for deletion", id);
        }

        return result;
    }
}