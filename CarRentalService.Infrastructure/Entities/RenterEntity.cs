using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalService.Infrastructure.Entities;

/// <summary>
/// Entity for renter in database
/// </summary>
public class RenterEntity
{
    public Guid Id { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public ICollection<RentalEntity> Rentals { get; set; } = new List<RentalEntity>();
}

/// <summary>
/// Entity configuration for renter
/// </summary>
public class RenterEntityConfiguration : IEntityTypeConfiguration<RenterEntity>
{
    public void Configure(EntityTypeBuilder<RenterEntity> builder)
    {
        builder.ToTable("renters");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.LicenseNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(r => r.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.DateOfBirth)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

        builder.HasIndex(r => r.LicenseNumber)
            .IsUnique();
    }
}
