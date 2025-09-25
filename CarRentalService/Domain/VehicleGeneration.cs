namespace CarRentalService.Domain.Domain;
/// <summary>
/// Represents a specific generation of a vehicle model
/// </summary>
public class VehicleGeneration
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
    public required decimal PricePerHour { get; set; }

    /// <summary>
    /// Navigation property to the vehicle model
    /// </summary>
    public required VehicleModel Model { get; set; }
}