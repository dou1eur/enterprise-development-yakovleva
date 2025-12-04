using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts.Vehicle;

/// <summary>
/// Request DTO for creating or updating a vehicle
/// </summary>
public sealed record VehicleRequest(
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
