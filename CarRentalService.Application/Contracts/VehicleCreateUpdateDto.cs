using System;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for creating or updating a vehicle
/// </summary>
public class VehicleCreateUpdateDto
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
    public Guid GenerationId { get; set; }
}