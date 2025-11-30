using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Collection response DTO for renters
/// </summary>
public class RenterCollectionResponse
{
    /// <summary>
    /// List of renters
    /// </summary>
    public List<RenterDto> Renters { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of RenterCollectionResponse
    /// </summary>
    public RenterCollectionResponse() { }

    /// <summary>
    /// Initializes a new instance of RenterCollectionResponse with specified renters
    /// </summary>
    /// <param name="renters">List of renter DTOs</param>
    public RenterCollectionResponse(List<RenterDto> renters)
    {
        Renters = renters;
    }
}