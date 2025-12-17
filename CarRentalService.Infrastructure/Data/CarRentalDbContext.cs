using CarRentalService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Data;

/// <summary>
/// DbContext for CarRentalService using PostgreSQL
/// </summary>
public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{
    public DbSet<Renter> Renters => Set<Renter>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();
    public DbSet<ModelGeneration> ModelGenerations => Set<ModelGeneration>();
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Renter>(builder =>
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(r => r.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.FullName)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(r => r.DateOfBirth)
                .IsRequired();

            builder.HasIndex(r => r.LicenseNumber)
                .IsUnique();
        });

        modelBuilder.Entity<VehicleModel>(builder =>
        {
            builder.HasKey(vm => vm.Id);
            builder.Property(vm => vm.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(vm => vm.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(vm => vm.DriveType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);
            builder.Property(vm => vm.SeatCount)
                .IsRequired();
            builder.Property(vm => vm.BodyType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);
            builder.Property(vm => vm.VehicleClass)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);
        });

        modelBuilder.Entity<ModelGeneration>(builder =>
        {
            builder.HasKey(mg => mg.Id);
            builder.Property(mg => mg.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            builder.Property(mg => mg.Year)
                .IsRequired();
            builder.Property(mg => mg.EngineVolume)
                .IsRequired()
                .HasPrecision(3, 1);
            builder.Property(mg => mg.Transmission)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);
            builder.Property(mg => mg.RentalPricePerHour)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.HasOne(mg => mg.VehicleModel)
                .WithMany()
                .HasForeignKey(mg => mg.VehicleModelId);
        });

        modelBuilder.Entity<Vehicle>(builder =>
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            builder.Property(v => v.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(v => v.Color)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(v => v.ModelGeneration)
                .WithMany()
                .HasForeignKey(v => v.GenerationId);

            builder.HasIndex(v => v.LicensePlate)
                .IsUnique();
        });

        modelBuilder.Entity<Rental>(builder =>
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasDefaultValueSql("gen_random_uuid()");
            builder.Property(r => r.RentStartTime)
                .IsRequired();
            builder.Property(r => r.DurationHours)
                .IsRequired();
            builder.Property(r => r.TotalCost)
                .HasPrecision(10, 2)
                .IsRequired();

            builder.HasOne(r => r.Vehicle)
                .WithMany()
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.Renter)
                .WithMany()
                .HasForeignKey(r => r.RenterId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
