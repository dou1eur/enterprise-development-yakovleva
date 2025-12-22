namespace CarRentalService.Application.Contracts.Rental;

/// <summary>
/// Response DTO for rental information
/// </summary>
public sealed record RentalResponse(
    /// <summary>
    /// Unique identifier for the rental
    /// </summary>
    Guid Id,

    /// <summary>
    /// Date and time when the rental starts
    /// </summary>
    DateTime RentStartTime,

    /// <summary>
    /// Duration of the rental in hours
    /// </summary>
    int RentalDurationHours,

    /// <summary>
    /// Total cost of the rental
    /// </summary>
    decimal TotalCost,

    /// <summary>
    /// Identifier of the rented vehicle
    /// </summary>
    Guid VehicleId,

    /// <summary>
    /// Identifier of the customer
    /// </summary>
    Guid RenterId
);
