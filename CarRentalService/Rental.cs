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
    /// Rented vehicle associated with this rental
    /// </summary>
    public required Vehicle Car { get; set; }

    /// <summary>
    /// Renter associated with this rental
    /// </summary>
    public required Renter Renter { get; set; }
}