using CarRentalService.Application.Contracts;
using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Mappings;

/// <summary>
/// Mapper for converting between domain entities and DTOs
/// </summary>
public static class CarRentalMapper
{
    // Domain to DTO mappings

    /// <summary>
    /// Converts a Renter domain entity to RenterDto
    /// </summary>
    /// <param name="renter">The renter domain entity</param>
    /// <returns>The renter DTO</returns>
    public static RenterDto ToDto(this Renter renter) => new()
    {
        Id = renter.Id,
        LicenseNumber = renter.LicenseNumber,
        FullName = renter.FullName,
        DateOfBirth = renter.DateOfBirth
    };

    /// <summary>
    /// Converts a Vehicle domain entity to VehicleDto
    /// </summary>
    /// <param name="vehicle">The vehicle domain entity</param>
    /// <returns>The vehicle DTO</returns>
    public static VehicleDto ToDto(this Vehicle vehicle) => new()
    {
        Id = vehicle.Id,
        LicensePlate = vehicle.LicensePlate,
        Color = vehicle.Color,
        ModelGenerationId = vehicle.GenerationId
    };

    /// <summary>
    /// Converts a VehicleModel domain entity to VehicleModelDto
    /// </summary>
    /// <param name="vehicleModel">The vehicle model domain entity</param>
    /// <returns>The vehicle model DTO</returns>
    public static VehicleModelDto ToDto(this VehicleModel vehicleModel) => new()
    {
        Id = vehicleModel.Id,
        Name = vehicleModel.Name,
        DriveType = vehicleModel.DriveType,
        SeatCount = vehicleModel.SeatCount,
        BodyType = vehicleModel.BodyType,
        VehicleClass = vehicleModel.VehicleClass
    };

    /// <summary>
    /// Converts a ModelGeneration domain entity to ModelGenerationDto
    /// </summary>
    /// <param name="modelGeneration">The model generation domain entity</param>
    /// <returns>The model generation DTO</returns>
    public static ModelGenerationDto ToDto(this ModelGeneration modelGeneration) => new()
    {
        Id = modelGeneration.Id,
        Year = modelGeneration.Year,
        EngineVolume = modelGeneration.EngineVolume,
        Transmission = modelGeneration.Transmission,
        RentalPricePerHour = modelGeneration.RentalPricePerHour,
        VehicleModelId = modelGeneration.VehicleModelId
    };

    /// <summary>
    /// Converts a Rental domain entity to RentalDto
    /// </summary>
    /// <param name="rental">The rental domain entity</param>
    /// <returns>The rental DTO</returns>
    public static RentalDto ToDto(this Rental rental) => new()
    {
        Id = rental.Id,
        RentStartTime = rental.RentStartTime,
        RentalDurationHours = rental.DurationHours,
        TotalCost = rental.TotalCost,
        VehicleId = rental.VehicleId,
        CustomerId = rental.RenterId
    };

    // Request to Domain mappings

    /// <summary>
    /// Converts a CreateRenterRequest to Renter domain entity
    /// </summary>
    /// <param name="request">The create renter request</param>
    /// <returns>The renter domain entity</returns>
    public static Renter ToDomain(this CreateRenterRequest request) => new()
    {
        LicenseNumber = request.LicenseNumber,
        FullName = request.FullName,
        DateOfBirth = request.DateOfBirth
    };

    /// <summary>
    /// Converts a CreateVehicleRequest to Vehicle domain entity
    /// </summary>
    /// <param name="request">The create vehicle request</param>
    /// <returns>The vehicle domain entity</returns>
    public static Vehicle ToDomain(this CreateVehicleRequest request) => new()
    {
        LicensePlate = request.LicensePlate,
        Color = request.Color,
        GenerationId = request.ModelGenerationId
    };

    /// <summary>
    /// Converts a CreateVehicleModelRequest to VehicleModel domain entity
    /// </summary>
    /// <param name="request">The create vehicle model request</param>
    /// <returns>The vehicle model domain entity</returns>
    public static VehicleModel ToDomain(this CreateVehicleModelRequest request) => new()
    {
        Name = request.Name,
        DriveType = request.DriveType,
        SeatCount = request.SeatCount,
        BodyType = request.BodyType,
        VehicleClass = request.VehicleClass
    };

    /// <summary>
    /// Converts a CreateModelGenerationRequest to ModelGeneration domain entity
    /// </summary>
    /// <param name="request">The create model generation request</param>
    /// <returns>The model generation domain entity</returns>
    public static ModelGeneration ToDomain(this CreateModelGenerationRequest request) => new()
    {
        Year = request.Year,
        EngineVolume = request.EngineVolume,
        Transmission = request.Transmission,
        RentalPricePerHour = request.RentalPricePerHour,
        VehicleModelId = request.VehicleModelId
    };

    /// <summary>
    /// Converts a CreateRentalRequest to Rental domain entity
    /// </summary>
    /// <param name="request">The create rental request</param>
    /// <returns>The rental domain entity</returns>
    public static Rental ToDomain(this CreateRentalRequest request) => new()
    {
        RentStartTime = request.RentStartTime,
        DurationHours = request.RentalDurationHours,
        VehicleId = request.VehicleId,
        RenterId = request.CustomerId,
        TotalCost = 0 // Will be calculated in service
    };

    // Collection response mappings

    /// <summary>
    /// Converts a list of renters to RenterCollectionResponse
    /// </summary>
    /// <param name="renters">List of renter domain entities</param>
    /// <returns>The renter collection response</returns>
    public static RenterCollectionResponse ToResponse(this List<Renter> renters) =>
        new(renters.Select(ToDto).ToList());

    /// <summary>
    /// Converts a list of vehicles to VehicleCollectionResponse
    /// </summary>
    /// <param name="vehicles">List of vehicle domain entities</param>
    /// <returns>The vehicle collection response</returns>
    public static VehicleCollectionResponse ToResponse(this List<Vehicle> vehicles) =>
        new(vehicles.Select(ToDto).ToList());

    /// <summary>
    /// Converts a list of vehicle models to VehicleModelCollectionResponse
    /// </summary>
    /// <param name="vehicleModels">List of vehicle model domain entities</param>
    /// <returns>The vehicle model collection response</returns>
    public static VehicleModelCollectionResponse ToResponse(this List<VehicleModel> vehicleModels) =>
        new(vehicleModels.Select(ToDto).ToList());

    /// <summary>
    /// Converts a list of model generations to ModelGenerationCollectionResponse
    /// </summary>
    /// <param name="modelGenerations">List of model generation domain entities</param>
    /// <returns>The model generation collection response</returns>
    public static ModelGenerationCollectionResponse ToResponse(this List<ModelGeneration> modelGenerations) =>
        new(modelGenerations.Select(ToDto).ToList());

    /// <summary>
    /// Converts a list of rentals to RentalCollectionResponse
    /// </summary>
    /// <param name="rentals">List of rental domain entities</param>
    /// <returns>The rental collection response</returns>
    public static RentalCollectionResponse ToResponse(this List<Rental> rentals) =>
        new(rentals.Select(ToDto).ToList());

    /// <summary>
    /// Converts a list of vehicle rental counts to VehicleRentalCountCollectionResponse
    /// </summary>
    /// <param name="vehicleRentalCounts">List of vehicle rental count DTOs</param>
    /// <returns>The vehicle rental count collection response</returns>
    public static VehicleRentalCountCollectionResponse ToResponse(this List<VehicleRentalCountDto> vehicleRentalCounts) =>
        new(vehicleRentalCounts);

    /// <summary>
    /// Converts a list of renter total spent to RenterTotalSpentCollectionResponse
    /// </summary>
    /// <param name="renterTotalSpents">List of renter total spent DTOs</param>
    /// <returns>The renter total spent collection response</returns>
    public static RenterTotalSpentCollectionResponse ToResponse(this List<RenterTotalSpentDto> renterTotalSpents) =>
        new(renterTotalSpents);
}