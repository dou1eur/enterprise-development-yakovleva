using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for updating renter information
/// </summary>
public class UpdateRenterRequest
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