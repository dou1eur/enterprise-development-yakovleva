using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// Collection response DTO for vehicle models
/// </summary>
public class VehicleModelCollectionResponse
{
    /// <summary>
    /// List of vehicle models
    /// </summary>
    public List<VehicleModelDto> VehicleModels { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of VehicleModelCollectionResponse
    /// </summary>
    public VehicleModelCollectionResponse() { }

    /// <summary>
    /// Initializes a new instance of VehicleModelCollectionResponse with specified vehicle models
    /// </summary>
    /// <param name="vehicleModels">List of vehicle model DTOs</param>
    public VehicleModelCollectionResponse(List<VehicleModelDto> vehicleModels)
    {
        VehicleModels = vehicleModels;
    }
}
