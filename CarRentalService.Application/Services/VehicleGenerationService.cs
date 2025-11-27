using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service implementation for managing vehicle generation operations
/// Handles business logic for vehicle generation CRUD operations
/// </summary>
public class VehicleGenerationService : IVehicleGenerationService
{
    private readonly IVehicleGenerationRepository _generationRepository;
    private readonly IVehicleModelRepository _modelRepository;

    /// <summary>
    /// Initializes a new instance of the VehicleGenerationService class
    /// </summary>
    /// <param name="generationRepository">The vehicle generation repository</param>
    /// <param name="modelRepository">The vehicle model repository</param>
    public VehicleGenerationService(
        IVehicleGenerationRepository generationRepository,
        IVehicleModelRepository modelRepository)
    {
        _generationRepository = generationRepository;
        _modelRepository = modelRepository;
    }

    /// <summary>
    /// Creates a new vehicle generation record
    /// </summary>
    /// <param name="dto">Data transfer object containing generation creation details</param>
    /// <returns>The created vehicle generation data transfer object</returns>
    /// <exception cref="ArgumentException">Thrown when vehicle model is not found</exception>
    public VehicleGenerationDto Create(VehicleGenerationCreateUpdateDto dto)
    {
        var model = _modelRepository.GetByIdAsync(dto.ModelId).Result;
        if (model == null)
            throw new ArgumentException($"Vehicle model with ID {dto.ModelId} not found");

        var generation = new VehicleGeneration
        {
            Year = dto.Year,
            EngineVolume = dto.EngineVolume,
            Transmission = dto.Transmission,
            PricePerHour = dto.PricePerHour,
            Model = model
        };

        _generationRepository.AddAsync(generation);

        return MapToDto(generation);
    }

    /// <summary>
    /// Retrieves a vehicle generation by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the generation</param>
    /// <returns>The vehicle generation data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when generation with specified ID is not found</exception>
    public VehicleGenerationDto Get(Guid id)
    {
        var generation = _generationRepository.GetByIdAsync(id).Result;
        if (generation == null)
            throw new KeyNotFoundException($"Vehicle generation with ID {id} not found");

        return MapToDto(generation);
    }

    /// <summary>
    /// Retrieves all vehicle generation records
    /// </summary>
    /// <returns>List of all vehicle generation data transfer objects</returns>
    public List<VehicleGenerationDto> GetAll()
    {
        var generations = _generationRepository.GetAllAsync().Result;
        return generations.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Updates an existing vehicle generation record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated generation details</param>
    /// <param name="id">The unique identifier of the generation to update</param>
    /// <returns>The updated vehicle generation data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when generation with specified ID is not found</exception>
    /// <exception cref="ArgumentException">Thrown when vehicle model is not found</exception>
    public VehicleGenerationDto Update(VehicleGenerationCreateUpdateDto dto, Guid id)
    {
        var existingGeneration = _generationRepository.GetByIdAsync(id).Result;
        if (existingGeneration == null)
            throw new KeyNotFoundException($"Vehicle generation with ID {id} not found");

        if (existingGeneration.Model.Id != dto.ModelId)
        {
            var model = _modelRepository.GetByIdAsync(dto.ModelId).Result;
            if (model == null)
                throw new ArgumentException($"Vehicle model with ID {dto.ModelId} not found");
            existingGeneration.Model = model;
        }

        existingGeneration.Year = dto.Year;
        existingGeneration.EngineVolume = dto.EngineVolume;
        existingGeneration.Transmission = dto.Transmission;
        existingGeneration.PricePerHour = dto.PricePerHour;

        _generationRepository.UpdateAsync(existingGeneration);

        return MapToDto(existingGeneration);
    }

    /// <summary>
    /// Deletes a vehicle generation record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the generation to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id)
    {
        return _generationRepository.DeleteAsync(id).Result;
    }

    /// <summary>
    /// Retrieves all generations for a specific vehicle model
    /// </summary>
    /// <param name="modelId">The unique identifier of the vehicle model</param>
    /// <returns>List of generation data transfer objects for the specified model</returns>
    public List<VehicleGenerationDto> GetByModelId(Guid modelId)
    {
        var generations = _generationRepository.GetByModelIdAsync(modelId).Result;
        return generations.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Retrieves generations by production year range
    /// </summary>
    /// <param name="startYear">The start year</param>
    /// <param name="endYear">The end year</param>
    /// <returns>List of generation data transfer objects within the specified year range</returns>
    public List<VehicleGenerationDto> GetByYearRange(int startYear, int endYear)
    {
        var generations = _generationRepository.GetByYearRangeAsync(startYear, endYear).Result;
        return generations.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Maps a VehicleGeneration domain entity to a VehicleGenerationDto data transfer object
    /// </summary>
    /// <param name="generation">The vehicle generation domain entity</param>
    /// <returns>The mapped vehicle generation data transfer object</returns>
    private static VehicleGenerationDto MapToDto(VehicleGeneration generation)
    {
        return new VehicleGenerationDto
        {
            Id = generation.Id,
            Year = generation.Year,
            EngineVolume = generation.EngineVolume,
            Transmission = generation.Transmission,
            PricePerHour = generation.PricePerHour,
            ModelId = generation.Model.Id,
            GenerationInfo = $"{generation.Model.Name} {generation.Year} {generation.EngineVolume}L"
        };
    }
}