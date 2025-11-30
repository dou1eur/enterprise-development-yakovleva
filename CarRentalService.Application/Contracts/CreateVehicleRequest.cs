using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for creating a vehicle
/// </summary>
public class CreateVehicleRequest
{
    /// <summary>
    /// License plate number of the vehicle
    /// </summary>
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Color of the vehicle
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the vehicle generation
    /// </summary>
    public Guid ModelGenerationId { get; set; }
}