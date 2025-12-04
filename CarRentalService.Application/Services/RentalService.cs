using CarRentalService.Application.Contracts.Rental;
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
    /// Initializes a new instance of the <see cref="RentalService"/> class
    /// </summary>
    public RentalService(
        IRentalRepository rentalRepository,
        IRenterRepository renterRepository,
        IVehicleRepository vehicleRepository,
        IModelGenerationRepository modelGenerationRepository)
    {
        _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        _renterRepository = renterRepository ?? throw new ArgumentNullException(nameof(renterRepository));
        _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        _modelGenerationRepository = modelGenerationRepository ?? throw new ArgumentNullException(nameof(modelGenerationRepository));
    }

    /// <summary>
    /// Creates a new rental record with validation and cost calculation
    /// </summary>
    public async Task<RentalResponse> CreateAsync(RentalRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var renter = await _renterRepository.GetByIdAsync(request.CustomerId);
        if (renter == null)
            throw new ArgumentException($"Renter with ID {request.CustomerId} does not exist", nameof(request.CustomerId));

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null)
            throw new ArgumentException($"Vehicle with ID {request.VehicleId} does not exist", nameof(request.VehicleId));

        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(vehicle.GenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation for vehicle {request.VehicleId} does not exist", nameof(request.VehicleId));

        var rental = request.ToDomain();
        rental.TotalCost = modelGeneration.RentalPricePerHour * rental.DurationHours;

        var createdRental = await _rentalRepository.AddAsync(rental);
        return createdRental.ToResponse();
    }

    /// <summary>
    /// Retrieves a rental by its unique identifier
    /// </summary>
    public async Task<RentalResponse?> GetAsync(Guid id)
    {
        var rental = await _rentalRepository.GetByIdAsync(id);
        return rental?.ToResponse();
    }

    /// <summary>
    /// Retrieves all rental records from the system
    /// </summary>
    public async Task<List<RentalResponse>> GetAllAsync()
    {
        var rentals = await _rentalRepository.GetAllAsync();
        return rentals.ToResponseList();
    }

    /// <summary>
    /// Updates an existing rental with validation and cost recalculation
    /// </summary>
    public async Task<RentalResponse?> UpdateAsync(Guid id, RentalRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var existingRental = await _rentalRepository.GetByIdAsync(id);
        if (existingRental == null)
            return null;

        var renter = await _renterRepository.GetByIdAsync(request.CustomerId);
        if (renter == null)
            throw new ArgumentException($"Renter with ID {request.CustomerId} does not exist", nameof(request.CustomerId));

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null)
            throw new ArgumentException($"Vehicle with ID {request.VehicleId} does not exist", nameof(request.VehicleId));

        var modelGeneration = await _modelGenerationRepository.GetByIdAsync(vehicle.GenerationId);
        if (modelGeneration == null)
            throw new ArgumentException($"Model generation for vehicle {request.VehicleId} does not exist", nameof(request.VehicleId));

        existingRental.RentStartTime = request.RentStartTime;
        existingRental.DurationHours = request.RentalDurationHours;
        existingRental.VehicleId = request.VehicleId;
        existingRental.RenterId = request.CustomerId;
        existingRental.TotalCost = modelGeneration.RentalPricePerHour * existingRental.DurationHours;

        var updatedRental = await _rentalRepository.UpdateAsync(existingRental);
        return updatedRental.ToResponse();
    }

    /// <summary>
    /// Deletes a rental record from the system
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _rentalRepository.DeleteAsync(id);
    }
}