using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Collection response DTO for vehicles
/// </summary>
public class VehicleCollectionResponse
{
    /// <summary>
    /// List of vehicles
    /// </summary>
    public List<VehicleDto> Vehicles { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of VehicleCollectionResponse
    /// </summary>
    public VehicleCollectionResponse() { }

    /// <summary>
    /// Initializes a new instance of VehicleCollectionResponse with specified vehicles
    /// </summary>
    /// <param name="vehicles">List of vehicle DTOs</param>
    public VehicleCollectionResponse(List<VehicleDto> vehicles)
    {
        Vehicles = vehicles;
    }
}
