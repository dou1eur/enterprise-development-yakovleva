using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Collection response DTO for vehicle rental counts
/// </summary>
public class VehicleRentalCountCollectionResponse
{
    /// <summary>
    /// List of vehicle rental counts
    /// </summary>
    public List<VehicleRentalCountDto> VehicleRentalCounts { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of VehicleRentalCountCollectionResponse
    /// </summary>
    public VehicleRentalCountCollectionResponse() { }

    /// <summary>
    /// Initializes a new instance of VehicleRentalCountCollectionResponse with specified vehicle rental counts
    /// </summary>
    /// <param name="vehicleRentalCounts">List of vehicle rental count DTOs</param>
    public VehicleRentalCountCollectionResponse(List<VehicleRentalCountDto> vehicleRentalCounts)
    {
        VehicleRentalCounts = vehicleRentalCounts;
    }
}