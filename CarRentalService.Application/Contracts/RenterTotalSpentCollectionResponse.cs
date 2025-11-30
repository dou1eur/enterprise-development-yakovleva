using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Collection response DTO for renter total spent
/// </summary>
public class RenterTotalSpentCollectionResponse
{
    /// <summary>
    /// List of renter total spent records
    /// </summary>
    public List<RenterTotalSpentDto> RenterTotalSpents { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of RenterTotalSpentCollectionResponse
    /// </summary>
    public RenterTotalSpentCollectionResponse() { }

    /// <summary>
    /// Initializes a new instance of RenterTotalSpentCollectionResponse with specified renter total spent records
    /// </summary>
    /// <param name="renterTotalSpents">List of renter total spent DTOs</param>
    public RenterTotalSpentCollectionResponse(List<RenterTotalSpentDto> renterTotalSpents)
    {
        RenterTotalSpents = renterTotalSpents;
    }
}
