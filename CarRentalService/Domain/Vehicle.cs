namespace CarRentalService.Domain;

/// <summary>
/// Describes a specific vehicle
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Unique identifier for the vehicle
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// License plate number of the vehicle
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Color of the vehicle
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Identifier of the vehicle generation
    /// </summary>
    public Guid GenerationId { get; set; }
}