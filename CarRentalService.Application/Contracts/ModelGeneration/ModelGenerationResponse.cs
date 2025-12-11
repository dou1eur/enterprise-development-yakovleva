using CarRentalService.Domain;

namespace CarRentalService.Application.Contracts.ModelGeneration;

/// <summary>
/// Response DTO for model generation information
/// </summary>
public sealed record ModelGenerationResponse(
    /// <summary>
    /// Unique identifier for the model generation
    /// </summary>
    Guid Id,

    /// <summary>
    /// Production year of this generation
    /// </summary>
    int Year,

    /// <summary>
    /// Engine displacement in liters
    /// </summary>
    double EngineVolume,

    /// <summary>
    /// Transmission type of the generation
    /// </summary>
    Transmission Transmission,

    /// <summary>
    /// Rental price per hour
    /// </summary>
    decimal RentalPricePerHour,

    /// <summary>
    /// Identifier of the vehicle model
    /// </summary>
    Guid VehicleModelId
);
