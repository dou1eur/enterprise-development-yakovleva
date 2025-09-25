namespace CarRentalService.Domain.Domain;
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
    /// Navigation property to the vehicle generation
    /// </summary>
    public required VehicleGeneration Generation { get; set; }
}