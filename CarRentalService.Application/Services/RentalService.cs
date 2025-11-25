using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service implementation for managing rental operations
/// Handles business logic for rental CRUD operations and cost calculations
/// </summary>
public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRenterRepository _renterRepository;

    /// <summary>
    /// Initializes a new instance of the RentalService class
    /// </summary>
    /// <param name="rentalRepository">The rental repository</param>
    /// <param name="vehicleRepository">The vehicle repository</param>
    /// <param name="renterRepository">The renter repository</param>
    public RentalService(
        IRentalRepository rentalRepository,
        IVehicleRepository vehicleRepository,
        IRenterRepository renterRepository)
    {
        _rentalRepository = rentalRepository;
        _vehicleRepository = vehicleRepository;
        _renterRepository = renterRepository;
    }

    /// <summary>
    /// Creates a new rental record after validating vehicle and renter existence
    /// </summary>
    /// <param name="dto">Data transfer object containing rental creation details</param>
    /// <returns>The created rental data transfer object</returns>
    /// <exception cref="ArgumentException">Thrown when vehicle or renter is not found</exception>
    public RentalDto Create(RentalCreateUpdateDto dto)
    {
        var vehicle = _vehicleRepository.GetByIdAsync(dto.CarId).Result;
        if (vehicle == null)
            throw new ArgumentException($"Vehicle with ID {dto.CarId} not found");

        var renter = _renterRepository.GetByIdAsync(dto.RenterId).Result;
        if (renter == null)
            throw new ArgumentException($"Renter with ID {dto.RenterId} not found");

        var rental = new Rental
        {
            RentStartTime = dto.RentStartTime,
            DurationHours = dto.DurationHours,
            Car = vehicle,
            Renter = renter
        };

        _rentalRepository.AddAsync(rental);

        return MapToDto(rental);
    }

    /// <summary>
    /// Retrieves a rental by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental</param>
    /// <returns>The rental data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when rental with specified ID is not found</exception>
    public RentalDto Get(Guid id)
    {
        var rental = _rentalRepository.GetByIdAsync(id).Result;
        if (rental == null)
            throw new KeyNotFoundException($"Rental with ID {id} not found");

        return MapToDto(rental);
    }

    /// <summary>
    /// Retrieves all rental records
    /// </summary>
    /// <returns>List of all rental data transfer objects</returns>
    public List<RentalDto> GetAll()
    {
        var rentals = _rentalRepository.GetAllAsync().Result;
        return rentals.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Updates an existing rental record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated rental details</param>
    /// <param name="id">The unique identifier of the rental to update</param>
    /// <returns>The updated rental data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when rental with specified ID is not found</exception>
    /// <exception cref="ArgumentException">Thrown when vehicle or renter is not found</exception>
    public RentalDto Update(RentalCreateUpdateDto dto, Guid id)
    {
        var existingRental = _rentalRepository.GetByIdAsync(id).Result;
        if (existingRental == null)
            throw new KeyNotFoundException($"Rental with ID {id} not found");

        if (existingRental.Car.Id != dto.CarId)
        {
            var vehicle = _vehicleRepository.GetByIdAsync(dto.CarId).Result;
            if (vehicle == null)
                throw new ArgumentException($"Vehicle with ID {dto.CarId} not found");
            existingRental.Car = vehicle;
        }

        if (existingRental.Renter.Id != dto.RenterId)
        {
            var renter = _renterRepository.GetByIdAsync(dto.RenterId).Result;
            if (renter == null)
                throw new ArgumentException($"Renter with ID {dto.RenterId} not found");
            existingRental.Renter = renter;
        }

        existingRental.RentStartTime = dto.RentStartTime;
        existingRental.DurationHours = dto.DurationHours;

        _rentalRepository.UpdateAsync(existingRental);

        return MapToDto(existingRental);
    }

    /// <summary>
    /// Deletes a rental record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the rental to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id)
    {
        return _rentalRepository.DeleteAsync(id).Result;
    }

    /// <summary>
    /// Retrieves all rentals for a specific renter
    /// </summary>
    /// <param name="renterId">The unique identifier of the renter</param>
    /// <returns>List of rental data transfer objects for the specified renter</returns>
    public List<RentalDto> GetByRenterId(Guid renterId)
    {
        var rentals = _rentalRepository.GetByRenterIdAsync(renterId).Result;
        return rentals.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Retrieves all rentals for a specific vehicle
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <returns>List of rental data transfer objects for the specified vehicle</returns>
    public List<RentalDto> GetByVehicleId(Guid vehicleId)
    {
        var rentals = _rentalRepository.GetByVehicleIdAsync(vehicleId).Result;
        return rentals.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Calculates the rental cost for a vehicle and duration
    /// </summary>
    /// <param name="vehicleId">The unique identifier of the vehicle</param>
    /// <param name="durationHours">The rental duration in hours</param>
    /// <returns>The calculated rental cost</returns>
    /// <exception cref="ArgumentException">Thrown when vehicle is not found</exception>
    public decimal CalculateRentalCost(Guid vehicleId, int durationHours)
    {
        var vehicle = _vehicleRepository.GetByIdAsync(vehicleId).Result;
        if (vehicle == null)
            throw new ArgumentException($"Vehicle with ID {vehicleId} not found");

        return vehicle.Generation.PricePerHour * durationHours;
    }

    /// <summary>
    /// Maps a Rental domain entity to a RentalDto data transfer object
    /// </summary>
    /// <param name="rental">The rental domain entity</param>
    /// <returns>The mapped rental data transfer object</returns>
    private static RentalDto MapToDto(Rental rental)
    {
        return new RentalDto
        {
            Id = rental.Id,
            RentStartTime = rental.RentStartTime,
            DurationHours = rental.DurationHours,
            TotalCost = rental.TotalCost,
            CarId = rental.Car.Id,
            RenterId = rental.Renter.Id,
            CarInfo = $"{rental.Car.Generation.Model.Name} ({rental.Car.Color})",
            RenterInfo = rental.Renter.FullName
        };
    }
}