using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application.Contracts;

public class RentalDto
{
    public Guid Id { get; set; }
    public DateTime RentStartTime { get; set; }
    public int DurationHours { get; set; }
    public decimal TotalCost { get; set; }
    public Guid CarId { get; set; }
    public Guid RenterId { get; set; }
    public string? CarInfo { get; set; }
    public string? RenterInfo { get; set; }
}