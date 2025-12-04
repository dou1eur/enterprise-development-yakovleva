using CarRentalService.Application.Contracts.Renter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts.Common;

/// <summary>
/// DTO for renter total spent information
/// </summary>
public sealed record RenterTotalSpentResponse(
    /// <summary>
    /// Renter information
    /// </summary>
    RenterResponse Renter,

    /// <summary>
    /// Total amount spent on rentals
    /// </summary>
    decimal TotalSpent
);