namespace CarRentalService.Domain;
/// <summary>
/// Represents a person who rents vehicles
/// </summary>
public class Renter
{
    /// <summary>
    /// Unique identifier for the renter
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Driver's license number of the renter
    /// </summary>
    public required string LicenseNumber { get; set; }

    /// <summary>
    /// Full name of the renter
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Date of birth of the renter
    /// </summary>
    public required DateTime DateOfBirth { get; set; }
}
