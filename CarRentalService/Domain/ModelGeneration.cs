namespace CarRentalService.Domain;

/// <summary>
/// Represents a specific generation of a vehicle model
/// </summary>
public class ModelGeneration
{
    /// <summary>
    /// Unique identifier for the generation
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Production year of this generation
    /// </summary>
    public required int Year { get; set; }

    /// <summary>
    /// Engine displacement in liters
    /// </summary>
    public required double EngineVolume { get; set; }

    /// <summary>
    /// Transmission type of the generation
    /// </summary>
    public required Transmission Transmission { get; set; }

    /// <summary>
    /// Rental price per hour
    /// </summary>
    public required decimal RentalPricePerHour { get; set; }

    /// <summary>
    /// Identifier of the vehicle model
    /// </summary>
    public Guid VehicleModelId { get; set; }
}