using CarRentalService.Domain;
using System;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for creating or updating a vehicle generation
/// </summary>
public class VehicleGenerationCreateUpdateDto
{
    /// <summary>
    /// Production year of this generation
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Engine displacement in liters
    /// </summary>
    public double EngineVolume { get; set; }

    /// <summary>
    /// Transmission type of the generation
    /// </summary>
    public Transmission Transmission { get; set; }

    /// <summary>
    /// Rental price per hour
    /// </summary>
    public decimal PricePerHour { get; set; }

    /// <summary>
    /// Identifier of the vehicle model
    /// </summary>
    public Guid ModelId { get; set; }
}