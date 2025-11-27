using CarRentalService.Application.Contracts;
using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService.Application.Services;

/// <summary>
/// Service implementation for managing vehicle model operations
/// Handles business logic for vehicle model CRUD operations
/// </summary>
public class VehicleModelService : IVehicleModelService
{
    private readonly IVehicleModelRepository _modelRepository;

    /// <summary>
    /// Initializes a new instance of the VehicleModelService class
    /// </summary>
    /// <param name="modelRepository">The vehicle model repository</param>
    public VehicleModelService(IVehicleModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    /// <summary>
    /// Creates a new vehicle model record
    /// </summary>
    /// <param name="dto">Data transfer object containing model creation details</param>
    /// <returns>The created vehicle model data transfer object</returns>
    public VehicleModelDto Create(VehicleModelCreateUpdateDto dto)
    {
        var model = new VehicleModel
        {
            Name = dto.Name,
            DriveType = dto.DriveType,
            SeatCount = dto.SeatCount,
            BodyType = dto.BodyType,
            VehicleClass = dto.VehicleClass
        };

        _modelRepository.AddAsync(model);

        return MapToDto(model);
    }

    /// <summary>
    /// Retrieves a vehicle model by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model</param>
    /// <returns>The vehicle model data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when model with specified ID is not found</exception>
    public VehicleModelDto Get(Guid id)
    {
        var model = _modelRepository.GetByIdAsync(id).Result;
        if (model == null)
            throw new KeyNotFoundException($"Vehicle model with ID {id} not found");

        return MapToDto(model);
    }

    /// <summary>
    /// Retrieves all vehicle model records
    /// </summary>
    /// <returns>List of all vehicle model data transfer objects</returns>
    public List<VehicleModelDto> GetAll()
    {
        var models = _modelRepository.GetAllAsync().Result;
        return models.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Updates an existing vehicle model record
    /// </summary>
    /// <param name="dto">Data transfer object containing updated model details</param>
    /// <param name="id">The unique identifier of the model to update</param>
    /// <returns>The updated vehicle model data transfer object</returns>
    /// <exception cref="KeyNotFoundException">Thrown when model with specified ID is not found</exception>
    public VehicleModelDto Update(VehicleModelCreateUpdateDto dto, Guid id)
    {
        var existingModel = _modelRepository.GetByIdAsync(id).Result;
        if (existingModel == null)
            throw new KeyNotFoundException($"Vehicle model with ID {id} not found");

        existingModel.Name = dto.Name;
        existingModel.DriveType = dto.DriveType;
        existingModel.SeatCount = dto.SeatCount;
        existingModel.BodyType = dto.BodyType;
        existingModel.VehicleClass = dto.VehicleClass;

        _modelRepository.UpdateAsync(existingModel);

        return MapToDto(existingModel);
    }

    /// <summary>
    /// Deletes a vehicle model record by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the model to delete</param>
    /// <returns>True if deletion was successful, otherwise false</returns>
    public bool Delete(Guid id)
    {
        return _modelRepository.DeleteAsync(id).Result;
    }

    /// <summary>
    /// Retrieves a vehicle model by its name
    /// </summary>
    /// <param name="name">The model name</param>
    /// <returns>The vehicle model data transfer object if found, otherwise null</returns>
    public VehicleModelDto? GetByName(string name)
    {
        var model = _modelRepository.GetByNameAsync(name).Result;
        return model != null ? MapToDto(model) : null;
    }

    /// <summary>
    /// Retrieves vehicle models by body type
    /// </summary>
    /// <param name="bodyType">The body type</param>
    /// <returns>List of vehicle model data transfer objects with the specified body type</returns>
    public List<VehicleModelDto> GetByBodyType(BodyType bodyType)
    {
        var models = _modelRepository.GetByBodyTypeAsync(bodyType).Result;
        return models.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Retrieves vehicle models by vehicle class
    /// </summary>
    /// <param name="vehicleClass">The vehicle class</param>
    /// <returns>List of vehicle model data transfer objects with the specified vehicle class</returns>
    public List<VehicleModelDto> GetByVehicleClass(VehicleClass vehicleClass)
    {
        var models = _modelRepository.GetByVehicleClassAsync(vehicleClass).Result;
        return models.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Maps a VehicleModel domain entity to a VehicleModelDto data transfer object
    /// </summary>
    /// <param name="model">The vehicle model domain entity</param>
    /// <returns>The mapped vehicle model data transfer object</returns>
    private static VehicleModelDto MapToDto(VehicleModel model)
    {
        return new VehicleModelDto
        {
            Id = model.Id,
            Name = model.Name,
            DriveType = model.DriveType,
            SeatCount = model.SeatCount,
            BodyType = model.BodyType,
            VehicleClass = model.VehicleClass,
            ModelInfo = $"{model.Name} ({model.BodyType}, {model.VehicleClass})"
        };
    }
}