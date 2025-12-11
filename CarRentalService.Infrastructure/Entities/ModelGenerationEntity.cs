using CarRentalService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.Entities;

/// <summary>
/// Entity for model generation in database
/// </summary>
public class ModelGenerationEntity
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public double EngineVolume { get; set; }
    public Transmission Transmission { get; set; }
    public decimal RentalPricePerHour { get; set; }
    public Guid VehicleModelId { get; set; }
    public VehicleModelEntity VehicleModel { get; set; } = null!;
    public ICollection<VehicleEntity> Vehicles { get; set; } = new List<VehicleEntity>();
}

/// <summary>
/// Entity configuration for model generation
/// </summary>
public class ModelGenerationEntityConfiguration : IEntityTypeConfiguration<ModelGenerationEntity>
{
    public void Configure(EntityTypeBuilder<ModelGenerationEntity> builder)
    {
        builder.ToTable("model_generations");

        builder.HasKey(mg => mg.Id);

        builder.Property(mg => mg.Year)
            .IsRequired();

        builder.Property(mg => mg.EngineVolume)
            .IsRequired()
            .HasColumnType("decimal(3,1)");

        builder.Property(mg => mg.Transmission)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(mg => mg.RentalPricePerHour)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.HasOne(mg => mg.VehicleModel)
            .WithMany(vm => vm.ModelGenerations)
            .HasForeignKey(mg => mg.VehicleModelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(mg => new { mg.VehicleModelId, mg.Year })
            .IsUnique();
    }
}
