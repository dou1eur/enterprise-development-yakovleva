using CarRentalService.Domain;

namespace CarRentalService.Application.Contracts.VehicleModel;

/// <summary>
/// Response DTO for vehicle model information
/// </summary>
public sealed record VehicleModelResponse(
    /// <summary>
    /// Unique identifier for the vehicle model
    /// </summary>
    Guid Id,

    /// <summary>
    /// Name of the model
    /// </summary>
    string Name,

    /// <summary>
    /// Drivetrain type of the vehicle
    /// </summary>
    Domain.DriveType DriveType,

    /// <summary>
    /// Number of passenger seats
    /// </summary>
    int SeatCount,

    /// <summary>
    /// Body type of the vehicle
    /// </summary>
    BodyType BodyType,

    /// <summary>
    /// Vehicle class category
    /// </summary>
    VehicleClass VehicleClass
);
