using CarRentalService.Application.Interfaces;
using CarRentalService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalService.Application;

/// <summary>
/// Provides extension methods for registering application layer services
/// in the dependency injection container
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Registers all application services with the service collection
    /// </summary>
    /// <param name="services">The service collection to which services will be added</param>
    /// <returns>The service collection with registered application services</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        services.AddScoped<IModelGenerationService, ModelGenerationService>();
        services.AddScoped<IRentalService, RentalService>();
        services.AddScoped<IRenterService, RenterService>();
        services.AddScoped<IVehicleModelService, VehicleModelService>();
        services.AddScoped<IVehicleService, VehicleService>();

        return services;
    }
}

