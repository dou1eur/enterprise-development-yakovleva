using CarRentalService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.Entities;

/// <summary>
/// Entity for vehicle model in database
/// </summary>
public class VehicleModelEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Domain.DriveType DriveType { get; set; }
    public int SeatCount { get; set; }
    public BodyType BodyType { get; set; }
    public VehicleClass VehicleClass { get; set; }
    public ICollection<ModelGenerationEntity> ModelGenerations { get; set; } = [];
}

/// <summary>
/// Entity configuration for vehicle model
/// </summary>
public class VehicleModelEntityConfiguration : IEntityTypeConfiguration<VehicleModelEntity>
{
    public void Configure(EntityTypeBuilder<VehicleModelEntity> builder)
    {
        builder.ToTable("vehicle_models");

        builder.HasKey(vm => vm.Id);

        builder.Property(vm => vm.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(vm => vm.DriveType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(vm => vm.SeatCount)
            .IsRequired();

        builder.Property(vm => vm.BodyType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(vm => vm.VehicleClass)
            .IsRequired()
            .HasConversion<int>();

        builder.HasIndex(vm => vm.Name)
            .IsUnique();
    }
}