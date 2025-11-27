using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service implementation for managing vehicle operations
/// Handles business logic for vehicle CRUD operations
/// </summary>
public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IVehicleGenerationRepository _generationRepository;

    /// <summary>
    /// Initializes a new instance of the VehicleService class
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository</param>
    /// <param name="generationRepository">The vehicle generation repository</param>
    public VehicleService(
        IVehicleRepository vehicleRepository,
        IVehicleGenerationRepository generationRepository)
    {
        _vehicleRepository = vehicleRepository;
        _generationRepository = generationRepository;
    }

    /// <summary>
    /// Creates a new vehicle record
    /// </summary>
    /// <param name="dto">Data transfer object containing vehicle creation details</param>
    /// <returns>The created vehicle data transfer object</returns>
    /// <exception cref="ArgumentException">Thrown when vehicle generation is not found</exception>
    public VehicleDto Create(VehicleCreateUpdateDto dto)
    {
        var generation = _generationRepository.GetByIdAsync(dto.GenerationId).Result;
        if (generation == null)
            throw new ArgumentException($"Vehicle generation with ID {dto.GenerationId} not found");

        var vehicle = new Vehicle
        {
            LicensePlate = dto.LicensePlate,
            Color = dto.Color,
            Generation = generation
        };

        _vehicleRepository.AddAsync(vehicle);

        return MapToDto(vehicle);
    }

    /// <summary>
    /// Retrieves a vehicle by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle</param>
    /// <returns>The vehicle data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when vehicle with specified ID is not found</exception>
    public VehicleDto Get(Guid id)
    {
        var vehicle = _vehicleRepository.GetByIdAsync(id).Result;
        if (vehicle == null)
            throw new KeyNotFoundException($"Vehicle with ID {id} not found");

        return MapToDto(vehicle);
    }

    /// <summary>
    /// Retrieves all vehicle records
    /// </summary>
    /// <returns>List of all vehicle data transfer objects</returns>
    public List<VehicleDto> GetAll()
    {
        var vehicles = _vehicleRepository.GetAllAsync().Result;
        return vehicles.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Updates an existing vehicle record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated vehicle details</param>
    /// <param name="id">The unique identifier of the vehicle to update</param>
    /// <returns>The updated vehicle data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when vehicle with specified ID is not found</exception>
    /// <exception cref="ArgumentException">Thrown when vehicle generation is not found</exception>
    public VehicleDto Update(VehicleCreateUpdateDto dto, Guid id)
    {
        var existingVehicle = _vehicleRepository.GetByIdAsync(id).Result;
        if (existingVehicle == null)
            throw new KeyNotFoundException($"Vehicle with ID {id} not found");

        if (existingVehicle.Generation.Id != dto.GenerationId)
        {
            var generation = _generationRepository.GetByIdAsync(dto.GenerationId).Result;
            if (generation == null)
                throw new ArgumentException($"Vehicle generation with ID {dto.GenerationId} not found");
            existingVehicle.Generation = generation;
        }

        existingVehicle.LicensePlate = dto.LicensePlate;
        existingVehicle.Color = dto.Color;

        _vehicleRepository.UpdateAsync(existingVehicle);

        return MapToDto(existingVehicle);
    }

    /// <summary>
    /// Deletes a vehicle record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the vehicle to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id)
    {
        return _vehicleRepository.DeleteAsync(id).Result;
    }

    /// <summary>
    /// Retrieves a vehicle by its license plate number
    /// </summary>
    /// <param name="licensePlate">The license plate number</param>
    /// <returns>The vehicle data transfer object if found, otherwise null</returns>
    public VehicleDto? GetByLicensePlate(string licensePlate)
    {
        var vehicle = _vehicleRepository.GetByLicensePlateAsync(licensePlate).Result;
        return vehicle != null ? MapToDto(vehicle) : null;
    }

    /// <summary>
    /// Retrieves all vehicles of a specific model
    /// </summary>
    /// <param name="modelId">The unique identifier of the vehicle model</param>
    /// <returns>List of vehicle data transfer objects for the specified model</returns>
    public List<VehicleDto> GetByModel(Guid modelId)
    {
        var vehicles = _vehicleRepository.GetByModelAsync(modelId).Result;
        return vehicles.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Maps a Vehicle domain entity to a VehicleDto data transfer object
    /// </summary>
    /// <param name="vehicle">The vehicle domain entity</param>
    /// <returns>The mapped vehicle data transfer object</returns>
    private static VehicleDto MapToDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Color = vehicle.Color,
            GenerationId = vehicle.Generation.Id,
            VehicleInfo = $"{vehicle.Generation.Model.Name} {vehicle.Generation.Year} ({vehicle.Color}) - {vehicle.LicensePlate}"
        };
    }
}