using CarRentalService.Domain;
using System;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for creating or updating a vehicle model
/// </summary>
public class VehicleModelCreateUpdateDto
{
    /// <summary>
    /// Name of the model
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Drivetrain type of the vehicle
    /// </summary>
    public Domain.DriveType DriveType { get; set; }

    /// <summary>
    /// Number of passenger seats
    /// </summary>
    public int SeatCount { get; set; }

    /// <summary>
    /// Body type of the vehicle
    /// </summary>
    public BodyType BodyType { get; set; }

    /// <summary>
    /// Vehicle class category
    /// </summary>
    public VehicleClass VehicleClass { get; set; }
}