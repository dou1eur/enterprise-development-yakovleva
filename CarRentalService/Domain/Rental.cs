namespace CarRentalService.Domain;

/// <summary>
/// Represents a rental transaction of a vehicle
/// </summary>
public class Rental
{
    /// <summary>
    /// Unique identifier for the rental transaction
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the rental starts
    /// </summary>
    public DateTime RentStartTime { get; set; }

    /// <summary>
    /// Duration of the rental (hours)
    /// </summary>
    public int DurationHours { get; set; }

    /// <summary>
    /// Identifier of the rented vehicle
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Identifier of the renter
    /// </summary>
    public Guid RenterId { get; set; }

    /// <summary>
    /// Total cost of the rental
    /// </summary>
    public decimal TotalCost { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Renter? Renter { get; set; }

}