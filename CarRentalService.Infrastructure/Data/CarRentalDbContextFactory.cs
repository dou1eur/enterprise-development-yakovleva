using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Infrastructure.Data;

/// <summary>
/// Factory for creating CarRentalDbContext during design time
/// Used by EF Core migrations
/// </summary>
public class CarRentalDbContextFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
{
    /// <summary>
    /// Creates a new instance of CarRentalDbContext
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>Configured CarRentalDbContext</returns>
    public CarRentalDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<CarRentalDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CarRentalDbContext(optionsBuilder.Options);
    }
}