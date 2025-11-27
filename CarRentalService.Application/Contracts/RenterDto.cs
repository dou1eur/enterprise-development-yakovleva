using System;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for renter information
/// </summary>
public class RenterDto
{
    /// <summary>
    /// Unique identifier for the renter
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Driver's license number of the renter
    /// </summary>
    public string LicenseNumber { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the renter
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the renter
    /// </summary>
    public DateTime DateOfBirth { get; set; }
}

/// <summary>
/// Data transfer object for creating or updating renter information
/// </summary>
public class RenterCreateUpdateDto
{
    /// <summary>
    /// Driver's license number of the renter
    /// </summary>
    public string LicenseNumber { get; set; } = string.Empty;

    /// <summary>
    /// Full name of the renter
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the renter
    /// </summary>
    public DateTime DateOfBirth { get; set; }
}