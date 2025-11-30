using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Data transfer object for creating a rental
/// </summary>
public class CreateRentalRequest
{
    /// <summary>
    /// Date and time when the rental starts
    /// </summary>
    public DateTime RentStartTime { get; set; }

    /// <summary>
    /// Duration of the rental in hours
    /// </summary>
    public int RentalDurationHours { get; set; }

    /// <summary>
    /// Identifier of the vehicle to rent
    /// </summary>
    public Guid VehicleId { get; set; }

    /// <summary>
    /// Identifier of the renter
    /// </summary>
    public Guid CustomerId { get; set; }
}
