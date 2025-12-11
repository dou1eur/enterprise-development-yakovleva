namespace CarRentalService.Application.Contracts.Vehicle;

/// <summary>
/// Response DTO for vehicle information
/// </summary>
public sealed record VehicleResponse(
    /// <summary>
    /// Unique identifier for the vehicle
    /// </summary>
    Guid Id,

    /// <summary>
    /// License plate number of the vehicle
    /// </summary>
    string LicensePlate,

    /// <summary>
    /// Color of the vehicle
    /// </summary>
    string Color,

    /// <summary>
    /// Identifier of the vehicle generation
    /// </summary>
    Guid ModelGenerationId
);