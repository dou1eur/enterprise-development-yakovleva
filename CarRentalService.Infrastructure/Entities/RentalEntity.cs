using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.Entities;

/// <summary>
/// Entity for rental in database
/// </summary>
public class RentalEntity
{
    public Guid Id { get; set; }
    public DateTime RentStartTime { get; set; }
    public int DurationHours { get; set; }
    public decimal TotalCost { get; set; }
    public Guid VehicleId { get; set; }
    public Guid RenterId { get; set; }
    public VehicleEntity Vehicle { get; set; } = null!;
    public RenterEntity Renter { get; set; } = null!;
}

/// <summary>
/// Entity configuration for rental
/// </summary>
public class RentalEntityConfiguration : IEntityTypeConfiguration<RentalEntity>
{
    public void Configure(EntityTypeBuilder<RentalEntity> builder)
    {
        builder.ToTable("rentals");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RentStartTime)
            .IsRequired();

        builder.Property(r => r.DurationHours)
            .IsRequired();

        builder.Property(r => r.TotalCost)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.HasOne(r => r.Vehicle)
            .WithMany(v => v.Rentals)
            .HasForeignKey(r => r.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Renter)
            .WithMany(r => r.Rentals)
            .HasForeignKey(r => r.RenterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => r.RentStartTime);
        builder.HasIndex(r => r.VehicleId);
        builder.HasIndex(r => r.RenterId);
    }
}