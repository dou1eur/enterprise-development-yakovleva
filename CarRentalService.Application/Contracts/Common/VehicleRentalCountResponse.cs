using CarRentalService.Application.Contracts.Vehicle;

namespace CarRentalService.Application.Contracts.Common;

/// <summary>
/// DTO for vehicle rental count information
/// </summary>
public sealed record VehicleRentalCountResponse(
    /// <summary>
    /// Vehicle information
    /// </summary>
    VehicleResponse Vehicle,

    /// <summary>
    /// Number of rentals for this vehicle
    /// </summary>
    int RentalCount
);
