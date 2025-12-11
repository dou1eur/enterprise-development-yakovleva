using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.Entities;

/// <summary>
/// Entity for vehicle in database
/// </summary>
public class VehicleEntity
{
    public Guid Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public Guid ModelGenerationId { get; set; }
    public ModelGenerationEntity ModelGeneration { get; set; } = null!;
    public ICollection<RentalEntity> Rentals { get; set; } = new List<RentalEntity>();
}

/// <summary>
/// Entity configuration for vehicle
/// </summary>
public class VehicleEntityConfiguration : IEntityTypeConfiguration<VehicleEntity>
{
    public void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.LicensePlate)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(v => v.Color)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasOne(v => v.ModelGeneration)
            .WithMany(mg => mg.Vehicles)
            .HasForeignKey(v => v.ModelGenerationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(v => v.LicensePlate)
            .IsUnique();
    }
}