using CarRentalService.Application.Interfaces.Repositories;
using CarRentalService.Infrastructure.Data;
using CarRentalService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentalService.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
                              ?? configuration.GetConnectionString("DefaultConnection")
                              ?? "Host=postgres;Port=5432;Database=carrentaldb;Username=postgres;Password=password;";

        services.AddDbContext<CarRentalDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IModelGenerationRepository, ModelGenerationRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IRenterRepository, RenterRepository>();
        services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();

        return services;
    }
}