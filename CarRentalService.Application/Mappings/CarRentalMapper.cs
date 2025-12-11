using CarRentalService.Application.Contracts.Common;
using CarRentalService.Application.Contracts.ModelGeneration;
using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Application.Contracts.Renter;
using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Contracts.VehicleModel;
using CarRentalService.Domain;

namespace CarRentalService.Application.Mappings;

/// <summary>
/// Provides mapping methods between domain entities and DTOs (Data Transfer Objects)
/// This static class contains extension methods for converting between domain models and contract DTOs
/// </summary>
public static class CarRentalMapper
{
    /// <summary>
    /// Converts a <see cref="ModelGeneration"/> domain entity to a <see cref="ModelGenerationResponse"/> DTO
    /// </summary>
    public static ModelGenerationResponse ToResponse(this ModelGeneration domain) =>
        new(
            domain.Id,
            domain.Year,
            domain.EngineVolume,
            domain.Transmission,
            domain.RentalPricePerHour,
            domain.VehicleModelId
        );

    /// <summary>
    /// Converts a <see cref="Rental"/> domain entity to a <see cref="RentalResponse"/> DTO
    /// </summary>
    public static RentalResponse ToResponse(this Rental domain) =>
        new(
            domain.Id,
            domain.RentStartTime,
            domain.DurationHours,
            domain.TotalCost,
            domain.VehicleId,
            domain.RenterId
        );

    /// <summary>
    /// Converts a <see cref="Renter"/> domain entity to a <see cref="RenterResponse"/> DTO
    /// </summary>
    public static RenterResponse ToResponse(this Renter domain) =>
        new(
            domain.Id,
            domain.LicenseNumber,
            domain.FullName,
            domain.DateOfBirth
        );

    /// <summary>
    /// Converts a <see cref="Vehicle"/> domain entity to a <see cref="VehicleResponse"/> DTO
    /// </summary>
    public static VehicleResponse ToResponse(this Vehicle domain) =>
        new(
            domain.Id,
            domain.LicensePlate,
            domain.Color,
            domain.GenerationId
        );

    /// <summary>
    /// Converts a <see cref="VehicleModel"/> domain entity to a <see cref="VehicleModelResponse"/> DTO
    /// </summary>
    public static VehicleModelResponse ToResponse(this VehicleModel domain) =>
        new(
            domain.Id,
            domain.Name,
            domain.DriveType,
            domain.SeatCount,
            domain.BodyType,
            domain.VehicleClass
        );

    /// <summary>
    /// Converts a <see cref="ModelGenerationRequest"/> DTO to a <see cref="ModelGeneration"/> domain entity
    /// </summary>
    public static ModelGeneration ToDomain(this ModelGenerationRequest request, Guid? id = null) =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            Year = request.Year,
            EngineVolume = request.EngineVolume,
            Transmission = request.Transmission,
            RentalPricePerHour = request.RentalPricePerHour,
            VehicleModelId = request.VehicleModelId
        };

    /// <summary>
    /// Converts a <see cref="RentalRequest"/> DTO to a <see cref="Rental"/> domain entity
    /// </summary>
    public static Rental ToDomain(this RentalRequest request, Guid? id = null) =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            RentStartTime = request.RentStartTime,
            DurationHours = request.RentalDurationHours,
            VehicleId = request.VehicleId,
            RenterId = request.CustomerId,
            TotalCost = 0
        };

    /// <summary>
    /// Converts a <see cref="RenterRequest"/> DTO to a <see cref="Renter"/> domain entity
    /// </summary>
    public static Renter ToDomain(this RenterRequest request, Guid? id = null) =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            LicenseNumber = request.LicenseNumber,
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth
        };

    /// <summary>
    /// Converts a <see cref="VehicleRequest"/> DTO to a <see cref="Vehicle"/> domain entity
    /// </summary>
    public static Vehicle ToDomain(this VehicleRequest request, Guid? id = null) =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            LicensePlate = request.LicensePlate,
            Color = request.Color,
            GenerationId = request.ModelGenerationId
        };

    /// <summary>
    /// Converts a <see cref="VehicleModelRequest"/> DTO to a <see cref="VehicleModel"/> domain entity
    /// </summary>
    public static VehicleModel ToDomain(this VehicleModelRequest request, Guid? id = null) =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            Name = request.Name,
            DriveType = request.DriveType,
            SeatCount = request.SeatCount,
            BodyType = request.BodyType,
            VehicleClass = request.VehicleClass
        };

    /// <summary>
    /// Converts a tuple of (Vehicle, int) to a <see cref="VehicleRentalCountResponse"/> DTO
    /// </summary>
    public static VehicleRentalCountResponse ToResponse(this (Vehicle Vehicle, int RentalCount) tuple) =>
        new(tuple.Vehicle.ToResponse(), tuple.RentalCount);

    /// <summary>
    /// Converts a tuple of (Renter, decimal) to a <see cref="RenterTotalSpentResponse"/> DTO
    /// </summary>
    public static RenterTotalSpentResponse ToResponse(this (Renter Renter, decimal TotalSpent) tuple) =>
        new(tuple.Renter.ToResponse(), tuple.TotalSpent);

    /// <summary>
    /// Converts a collection of <see cref="ModelGeneration"/> entities to a list of <see cref="ModelGenerationResponse"/> DTOs
    /// </summary>
    public static List<ModelGenerationResponse> ToResponseList(this IEnumerable<ModelGeneration> domains) =>
        domains.Select(ToResponse).ToList();

    /// <summary>
    /// Converts a collection of <see cref="Rental"/> entities to a list of <see cref="RentalResponse"/> DTOs
    /// </summary>
    public static List<RentalResponse> ToResponseList(this IEnumerable<Rental> domains) =>
        domains.Select(ToResponse).ToList();

    /// <summary>
    /// Converts a collection of <see cref="Renter"/> entities to a list of <see cref="RenterResponse"/> DTOs
    /// </summary>
    public static List<RenterResponse> ToResponseList(this IEnumerable<Renter> domains) =>
        domains.Select(ToResponse).ToList();

    /// <summary>
    /// Converts a collection of <see cref="Vehicle"/> entities to a list of <see cref="VehicleResponse"/> DTOs
    /// </summary>
    public static List<VehicleResponse> ToResponseList(this IEnumerable<Vehicle> domains) =>
        domains.Select(ToResponse).ToList();

    /// <summary>
    /// Converts a collection of <see cref="VehicleModel"/> entities to a list of <see cref="VehicleModelResponse"/> DTOs
    /// </summary>
    public static List<VehicleModelResponse> ToResponseList(this IEnumerable<VehicleModel> domains) =>
        domains.Select(ToResponse).ToList();

    /// <summary>
    /// Converts a collection of vehicle rental count tuples to a list of <see cref="VehicleRentalCountResponse"/> DTOs
    /// </summary>
    public static List<VehicleRentalCountResponse> ToResponseList(this IEnumerable<(Vehicle Vehicle, int RentalCount)> tuples) =>
        tuples.Select(ToResponse).ToList();

    /// <summary>
    /// Converts a collection of renter total spent tuples to a list of <see cref="RenterTotalSpentResponse"/> DTOs
    /// </summary>
    public static List<RenterTotalSpentResponse> ToResponseList(this IEnumerable<(Renter Renter, decimal TotalSpent)> tuples) =>
        tuples.Select(ToResponse).ToList();

    /// <summary>
    /// Creates a <see cref="CollectionResponse{T}"/> from a collection of items
    /// </summary>
    public static CollectionResponse<T> ToCollectionResponse<T>(this IEnumerable<T> items) =>
        new(items.ToList());
}
