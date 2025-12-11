namespace CarRentalService.Application.Contracts.Rental;

/// <summary>
/// Request DTO for creating or updating a rental
/// </summary>
public sealed record RentalRequest(
    /// <summary>
    /// Date and time when the rental starts
    /// </summary>
    DateTime RentStartTime,

    /// <summary>
    /// Duration of the rental in hours
    /// </summary>
    int RentalDurationHours,

    /// <summary>
    /// Identifier of the vehicle to rent
    /// </summary>
    Guid VehicleId,

    /// <summary>
    /// Identifier of the renter
    /// </summary>
    Guid CustomerId
);