using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

public class RentalCreateUpdateDto
{
    public DateTime RentStartTime { get; set; }
    public int DurationHours { get; set; }
    public Guid CarId { get; set; }
    public Guid RenterId { get; set; }
}