using CarRentalService.Domain;
using CarRentalService.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.Mappings;

/// <summary>
/// Mapper for converting between domain entities and database entities
/// </summary>
public static class EntityMapper
{
    // Domain to Entity mappings

    /// <summary>
    /// Converts a Renter domain entity to RenterEntity
    /// </summary>
    /// <param name="renter">The renter domain entity</param>
    /// <returns>The renter database entity</returns>
    public static RenterEntity ToEntity(this Renter renter) => new()
    {
        Id = renter.Id,
        LicenseNumber = renter.LicenseNumber,
        FullName = renter.FullName,
        DateOfBirth = renter.DateOfBirth
    };

    /// <summary>
    /// Converts a Vehicle domain entity to VehicleEntity
    /// </summary>
    /// <param name="vehicle">The vehicle domain entity</param>
    /// <returns>The vehicle database entity</returns>
    public static VehicleEntity ToEntity(this Vehicle vehicle) => new()
    {
        Id = vehicle.Id,
        LicensePlate = vehicle.LicensePlate,
        Color = vehicle.Color,
        ModelGenerationId = vehicle.GenerationId
    };

    /// <summary>
    /// Converts a VehicleModel domain entity to VehicleModelEntity
    /// </summary>
    /// <param name="vehicleModel">The vehicle model domain entity</param>
    /// <returns>The vehicle model database entity</returns>
    public static VehicleModelEntity ToEntity(this VehicleModel vehicleModel) => new()
    {
        Id = vehicleModel.Id,
        Name = vehicleModel.Name,
        DriveType = vehicleModel.DriveType,
        SeatCount = vehicleModel.SeatCount,
        BodyType = vehicleModel.BodyType,
        VehicleClass = vehicleModel.VehicleClass
    };

    /// <summary>
    /// Converts a ModelGeneration domain entity to ModelGenerationEntity
    /// </summary>
    /// <param name="modelGeneration">The model generation domain entity</param>
    /// <returns>The model generation database entity</returns>
    public static ModelGenerationEntity ToEntity(this ModelGeneration modelGeneration) => new()
    {
        Id = modelGeneration.Id,
        Year = modelGeneration.Year,
        EngineVolume = modelGeneration.EngineVolume,
        Transmission = modelGeneration.Transmission,
        RentalPricePerHour = modelGeneration.RentalPricePerHour,
        VehicleModelId = modelGeneration.VehicleModelId
    };

    /// <summary>
    /// Converts a Rental domain entity to RentalEntity
    /// </summary>
    /// <param name="rental">The rental domain entity</param>
    /// <returns>The rental database entity</returns>
    public static RentalEntity ToEntity(this Rental rental) => new()
    {
        Id = rental.Id,
        RentStartTime = rental.RentStartTime,
        DurationHours = rental.DurationHours,
        TotalCost = rental.TotalCost,
        VehicleId = rental.VehicleId,
        RenterId = rental.RenterId
    };

    /// <summary>
    /// Converts a RenterEntity to Renter domain entity
    /// </summary>
    /// <param name="entity">The renter database entity</param>
    /// <returns>The renter domain entity</returns>
    public static Renter ToDomain(this RenterEntity entity) => new()
    {
        Id = entity.Id,
        LicenseNumber = entity.LicenseNumber,
        FullName = entity.FullName,
        DateOfBirth = entity.DateOfBirth
    };

    /// <summary>
    /// Converts a VehicleEntity to Vehicle domain entity
    /// </summary>
    /// <param name="entity">The vehicle database entity</param>
    /// <returns>The vehicle domain entity</returns>
    public static Vehicle ToDomain(this VehicleEntity entity) => new()
    {
        Id = entity.Id,
        LicensePlate = entity.LicensePlate,
        Color = entity.Color,
        GenerationId = entity.ModelGenerationId
    };

    /// <summary>
    /// Converts a VehicleModelEntity to VehicleModel domain entity
    /// </summary>
    /// <param name="entity">The vehicle model database entity</param>
    /// <returns>The vehicle model domain entity</returns>
    public static VehicleModel ToDomain(this VehicleModelEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        DriveType = entity.DriveType,
        SeatCount = entity.SeatCount,
        BodyType = entity.BodyType,
        VehicleClass = entity.VehicleClass
    };

    /// <summary>
    /// Converts a ModelGenerationEntity to ModelGeneration domain entity
    /// </summary>
    /// <param name="entity">The model generation database entity</param>
    /// <returns>The model generation domain entity</returns>
    public static ModelGeneration ToDomain(this ModelGenerationEntity entity) => new()
    {
        Id = entity.Id,
        Year = entity.Year,
        EngineVolume = entity.EngineVolume,
        Transmission = entity.Transmission,
        RentalPricePerHour = entity.RentalPricePerHour,
        VehicleModelId = entity.VehicleModelId
    };

    /// <summary>
    /// Converts a RentalEntity to Rental domain entity
    /// </summary>
    /// <param name="entity">The rental database entity</param>
    /// <returns>The rental domain entity</returns>
    public static Rental ToDomain(this RentalEntity entity) => new()
    {
        Id = entity.Id,
        RentStartTime = entity.RentStartTime,
        DurationHours = entity.DurationHours,
        TotalCost = entity.TotalCost,
        VehicleId = entity.VehicleId,
        RenterId = entity.RenterId
    };
}