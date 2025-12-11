namespace CarRentalService.Application.Contracts.Renter;

/// <summary>
/// Response DTO for renter information
/// </summary>
public sealed record RenterResponse(
    /// <summary>
    /// Unique identifier for the renter
    /// </summary>
    Guid Id,

    /// <summary>
    /// Driver's license number of the renter
    /// </summary>
    string LicenseNumber,

    /// <summary>
    /// Full name of the renter
    /// </summary>
    string FullName,

    /// <summary>
    /// Date of birth of the renter
    /// </summary>
    DateTime DateOfBirth
);