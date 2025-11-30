using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

/// <summary>
/// DTO for vehicle rental count information
/// </summary>
public sealed record VehicleRentalCountDto(VehicleDto Vehicle, int RentalCount);
