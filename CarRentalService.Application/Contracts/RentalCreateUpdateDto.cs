using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for creating or updating a rental
/// </summary>
public class RentalCreateUpdateDto
{
    /// <summary>
    /// Date and time when the rental starts
    /// </summary>
    public DateTime RentStartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours
    /// </summary>
    public int DurationHours { get; set; }

    /// <summary>
    /// Identifier of the vehicle to rent
    /// </summary>
    public Guid CarId { get; set; }

    /// <summary>
    /// Identifier of the renter
    /// </summary>
    public Guid RenterId { get; set; }
}