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
/// Service for managing rentals in the car rental system
/// Handles rental operations including validation and cost calculation
/// </summary>
public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IRenterRepository _renterRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IModelGenerationRepository _modelGenerationRepository;

    /// <summary>
    /// Initializes a new instance of RentalService
    /// </summary>
    /// <param name="rentalRepository">The rental repository</param>
    /// <param name="renterRepository">The renter repository</param>
    /// <param name="vehicleRepository">The vehicle repository</param>
    /// <param name="modelGenerationRepository">The model generation repository</param>
    public RentalService(
        IRentalRepository rentalRepository,
        IRenterRepository renterRepository,
        IVehicleRepository vehicleRepository,
        IModelGenerationRepository modelGenerationRepository)
    {
        _rentalRepository = rentalRepository;
        _renterRepository = renterRepository;
        _vehicleRepository = vehicleRepository;
        _modelGenerationRepository = modelGenerationRepository;
    }

    /// <summary>
    /// Creates a new rental record with validation and cost calculation
    /// </summary>
    /// <param name="request">Data transfer object containing rental creation details</param>
    /// <returns>The created rental data transfer object</returns>
    /// <exception cref="ArgumentException">Thrown when renter, vehicle, or model generation does not exist</exception>
    public async Task<RentalDto> CreateAsync(CreateRentalRequest request)
    {
        var renter = await _renterRepository.GetByIdAsync(request.CustomerId);
        if (renter == null)
            throw new ArgumentException($"Renter with ID {request.CustomerId} does not exist");

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null)
            throw new ArgumentException($"Vehicle with ID {request.VehicleId} does not exist");

        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(vehicle.GenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation for vehicle {request.VehicleId} does not exist");

        var rental = request.ToDomain();
        rental.TotalCost = modelGeneration.RentalPricePerHour * rental.DurationHours;

        var createdRental = await _rentalRepository.AddAsync(rental);
        return createdRental.ToDto();
    }

    /// <summary>
    /// Retrieves a rental by unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental</param>
    /// <returns>The rental data transfer object if found; otherwise, null</returns>
    public async Task<RentalDto?> GetAsync(Guid id)
    {
        var rental = await _rentalRepository.GetByIdAsync(id);
        return rental?.ToDto();
    }

    /// <summary>
    /// Retrieves all rental records
    /// </summary>
    /// <returns>List of all rental data transfer objects</returns>
    public async Task<List<RentalDto>> GetAllAsync()
    {
        var rentals = await _rentalRepository.GetAllAsync();
        return rentals.ConvertAll(r => r.ToDto());
    }

    /// <summary>
    /// Updates an existing rental record with validation and cost recalculation
    /// </summary>
    /// <param name="id">The unique identifier of the rental to update</param>
    /// <param name="request">Data transfer object containing updated rental details</param>
    /// <returns>The updated rental data transfer object if successful; otherwise, null</returns>
    /// <exception cref="ArgumentException">Thrown when renter, vehicle, or model generation does not exist</exception>
    public async Task<RentalDto?> UpdateAsync(Guid id, UpdateRentalRequest request)
    {
        var existingRental = await _rentalRepository.GetByIdAsync(id);
        if (existingRental == null)
            return null;

        var renter = await _renterRepository.GetByIdAsync(request.CustomerId);
        if (renter == null)
            throw new ArgumentException($"Renter with ID {request.CustomerId} does not exist");

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null)
            throw new ArgumentException($"Vehicle with ID {request.VehicleId} does not exist");

        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(vehicle.GenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation for vehicle {request.VehicleId} does not exist");

        existingRental.RentStartTime = request.RentStartTime;
        existingRental.DurationHours = request.RentalDurationHours;
        existingRental.VehicleId = request.VehicleId;
        existingRental.RenterId = request.CustomerId;
        existingRental.TotalCost = modelGeneration.RentalPricePerHour * existingRental.DurationHours;

        var updatedRental = await _rentalRepository.UpdateAsync(existingRental);
        return updatedRental.ToDto();
    }

    /// <summary>
    /// Deletes a rental record by identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _rentalRepository.DeleteAsync(id);
    }
}