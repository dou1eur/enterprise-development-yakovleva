using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.Data;

/// <summary>
/// DbContext for CarRentalService using PostgreSQL
/// </summary>
public class CarRentalDbContext : DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<RenterEntity> Renters { get; set; }
    public DbSet<VehicleEntity> Vehicles { get; set; }
    public DbSet<VehicleModelEntity> VehicleModels { get; set; }
    public DbSet<ModelGenerationEntity> ModelGenerations { get; set; }
    public DbSet<RentalEntity> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply entity configurations
        modelBuilder.ApplyConfiguration(new RenterEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleModelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ModelGenerationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RentalEntityConfiguration());
    }
}
