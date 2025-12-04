using CarRentalService.Application.Contracts.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
