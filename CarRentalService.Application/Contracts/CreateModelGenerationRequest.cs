using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for creating a model generation
/// </summary>
public class CreateModelGenerationRequest
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
    public decimal RentalPricePerHour { get; set; }

    /// <summary>
    /// Identifier of the vehicle model
    /// </summary>
    public Guid VehicleModelId { get; set; }
}