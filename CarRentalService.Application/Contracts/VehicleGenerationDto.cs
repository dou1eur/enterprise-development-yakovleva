using CarRentalService.Domain;
using System;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for vehicle generation information
/// </summary>
public class VehicleGenerationDto
{
    /// <summary>
    /// Unique identifier for the vehicle generation
    /// </summary>
    public Guid Id { get; set; }

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

    /// <summary>
    /// Additional information about the generation
    /// </summary>
    public string? GenerationInfo { get; set; }
}