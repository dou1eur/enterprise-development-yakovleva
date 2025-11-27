using System;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for vehicle information
/// </summary>
public class VehicleDto
{
    /// <summary>
    /// Unique identifier for the vehicle
    /// </summary>
    public Guid Id { get; set; }

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

    /// <summary>
    /// Additional information about the vehicle
    /// </summary>
    public string? VehicleInfo { get; set; }
}