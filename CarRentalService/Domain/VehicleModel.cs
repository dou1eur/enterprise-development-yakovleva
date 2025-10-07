namespace CarRentalService.Domain;

/// <summary>
/// Represents a vehicle model
/// </summary>
public class VehicleModel
{
    /// <summary>
    /// Unique identifier for the vehicle model
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Name of the model
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Drivetrain type of the vehicle
    /// </summary>
    public DriveType DriveType { get; set; }

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
