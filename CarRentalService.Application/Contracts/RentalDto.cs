using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for rental information
/// </summary>
public class RentalDto
{
    /// <summary>
    /// Unique identifier for the rental
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Date and time when the rental starts
    /// </summary>
    public DateTime RentStartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours
    /// </summary>
    public int DurationHours { get; set; }

    /// <summary>
    /// Total cost of the rental
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Identifier of the rented vehicle
    /// </summary>
    public Guid CarId { get; set; }

    /// <summary>
    /// Identifier of the renter
    /// </summary>
    public Guid RenterId { get; set; }

    /// <summary>
    /// Additional information about the rented vehicle
    /// </summary>
    public string? CarInfo { get; set; }

    /// <summary>
    /// Additional information about the renter
    /// </summary>
    public string? RenterInfo { get; set; }
}