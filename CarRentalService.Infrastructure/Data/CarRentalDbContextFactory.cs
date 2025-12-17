using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CarRentalService.Infrastructure.Data;

/// <summary>
/// Factory for creating CarRentalDbContext during design time
/// Used by EF Core migrations
/// </summary>
public class CarRentalDbContextFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
{
    public CarRentalDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=carrentaldb;Username=postgres;Password=password;";

        var optionsBuilder = new DbContextOptionsBuilder<CarRentalDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CarRentalDbContext(optionsBuilder.Options);
    }
}