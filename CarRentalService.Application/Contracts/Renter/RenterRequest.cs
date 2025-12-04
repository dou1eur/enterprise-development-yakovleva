using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts.Renter;

/// <summary>
/// Request DTO for creating or updating a renter
/// </summary>
public sealed record RenterRequest(
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