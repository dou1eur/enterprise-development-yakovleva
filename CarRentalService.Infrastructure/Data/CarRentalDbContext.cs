using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Data;

/// <summary>
/// DbContext for CarRentalService using PostgreSQL
/// </summary>
public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{
    public DbSet<RenterEntity> Renters { get; set; } = null!;
    public DbSet<VehicleEntity> Vehicles { get; set; } = null!;
    public DbSet<VehicleModelEntity> VehicleModels { get; set; } = null!;
    public DbSet<ModelGenerationEntity> ModelGenerations { get; set; } = null!;
    public DbSet<RentalEntity> Rentals { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RenterEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleModelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ModelGenerationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RentalEntityConfiguration());
    }
}
