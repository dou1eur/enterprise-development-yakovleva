using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for updating a vehicle model
/// </summary>
public class UpdateVehicleModelRequest
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