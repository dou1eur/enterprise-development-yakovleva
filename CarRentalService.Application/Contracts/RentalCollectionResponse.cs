using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Collection response DTO for rentals
/// </summary>
public class RentalCollectionResponse
{
    /// <summary>
    /// List of rentals
    /// </summary>
    public List<RentalDto> Rentals { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of RentalCollectionResponse
    /// </summary>
    public RentalCollectionResponse() { }

    /// <summary>
    /// Initializes a new instance of RentalCollectionResponse with specified rentals
    /// </summary>
    /// <param name="rentals">List of rental DTOs</param>
    public RentalCollectionResponse(List<RentalDto> rentals)
    {
        Rentals = rentals;
    }
}
