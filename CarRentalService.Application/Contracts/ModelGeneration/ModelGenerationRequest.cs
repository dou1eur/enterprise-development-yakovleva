using CarRentalService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts.ModelGeneration;

/// <summary>
/// Request DTO for creating or updating a model generation
/// </summary>
public sealed record ModelGenerationRequest(
    /// <summary>
    /// Production year of this generation
    /// </summary>
    int Year,

    /// <summary>
    /// Engine displacement in liters
    /// </summary>
    double EngineVolume,

    /// <summary>
    /// Transmission type of the generation
    /// </summary>
    Transmission Transmission,

    /// <summary>
    /// Rental price per hour
    /// </summary>
    decimal RentalPricePerHour,

    /// <summary>
    /// Identifier of the vehicle model
    /// </summary>
    Guid VehicleModelId
);
