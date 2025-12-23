using CarRentalService.Domain;
using CarRentalService.TestData.Constants;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Data;

/// <summary>
/// Provides seed data for the CarRentalService database
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Seeds the database with initial test data
    /// </summary>
    /// <param name="context">The database context</param>
    public static async Task SeedAsync(CarRentalDbContext context)
    {
        try
        {
            if (await context.VehicleModels.AnyAsync())
            {
                Console.WriteLine("Database already seeded");
                return;
            }

            Console.WriteLine("Starting database seed");

            TestDataConstants.Validate();

            await SeedVehicleModelsAsync(context);
            await SeedModelGenerationsAsync(context);
            await SeedVehiclesAsync(context);
            await SeedRentersAsync(context);
            await SeedRentalsAsync(context);

            await context.SaveChangesAsync();

            Console.WriteLine("Database successfully seeded with test data");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding database: {ex.Message}");
            throw;
        }
    }

    private static async Task SeedVehicleModelsAsync(CarRentalDbContext context)
    {
        var vehicleModels = new[]
        {
            new VehicleModel
            {
                Id = VehicleModelIds.ChevroletCobaltModel,
                Name = "Chevrolet Cobalt",
                DriveType = Domain.DriveType.Fwd,
                SeatCount = 5,
                BodyType = BodyType.Sedan,
                VehicleClass = VehicleClass.Compact
            },
            new VehicleModel
            {
                Id = VehicleModelIds.ToyotaCamryModel,
                Name = "Toyota Camry",
                DriveType = Domain.DriveType.Fwd,
                SeatCount = 5,
                BodyType = BodyType.Sedan,
                VehicleClass = VehicleClass.Family
            },
            new VehicleModel
            {
                Id = VehicleModelIds.BmwX5Model,
                Name = "BMW X5",
                DriveType = Domain.DriveType.Awd,
                SeatCount = 7,
                BodyType = BodyType.Suv,
                VehicleClass = VehicleClass.Luxury
            },
            new VehicleModel
            {
                Id = VehicleModelIds.FordTransitModel,
                Name = "Ford Transit",
                DriveType = Domain.DriveType.Rwd,
                SeatCount = 9,
                BodyType = BodyType.Minivan,
                VehicleClass = VehicleClass.Family
            },
            new VehicleModel
            {
                Id = VehicleModelIds.TeslaModel3,
                Name = "Tesla Model 3",
                DriveType = Domain.DriveType.Awd,
                SeatCount = 5,
                BodyType = BodyType.Sedan,
                VehicleClass = VehicleClass.Luxury
            }
        };

        await context.VehicleModels.AddRangeAsync(vehicleModels);
    }

    private static async Task SeedModelGenerationsAsync(CarRentalDbContext context)
    {
        var modelGenerations = new[]
        {
            new ModelGeneration
            {
                Id = ModelGenerationIds.CobaltGeneration,
                Year = 2016,
                EngineVolume = 1.8,
                Transmission = Transmission.Automatic,
                RentalPricePerHour = 1200.00m,
                VehicleModelId = VehicleModelIds.ChevroletCobaltModel
            },
            new ModelGeneration
            {
                Id = ModelGenerationIds.CamryGeneration,
                Year = 2023,
                EngineVolume = 2.0,
                Transmission = Transmission.Automatic,
                RentalPricePerHour = 1500.00m,
                VehicleModelId = VehicleModelIds.ToyotaCamryModel
            },
            new ModelGeneration
            {
                Id = ModelGenerationIds.BmwX5Generation,
                Year = 2024,
                EngineVolume = 3.0,
                Transmission = Transmission.Automatic,
                RentalPricePerHour = 3500.00m,
                VehicleModelId = VehicleModelIds.BmwX5Model
            },
            new ModelGeneration
            {
                Id = ModelGenerationIds.FordTransitGeneration,
                Year = 2022,
                EngineVolume = 2.2,
                Transmission = Transmission.Manual,
                RentalPricePerHour = 1800.00m,
                VehicleModelId = VehicleModelIds.FordTransitModel
            },
            new ModelGeneration
            {
                Id = ModelGenerationIds.TeslaGeneration,
                Year = 2023,
                EngineVolume = 0,
                Transmission = Transmission.Automatic,
                RentalPricePerHour = 2800.00m,
                VehicleModelId = VehicleModelIds.TeslaModel3
            }
        };

        await context.ModelGenerations.AddRangeAsync(modelGenerations);
    }

    private static async Task SeedVehiclesAsync(CarRentalDbContext context)
    {
        var vehicles = new[]
        {
            new Vehicle
            {
                Id = VehicleIds.CobaltVehicle1,
                LicensePlate = "H0990P",
                Color = "Black",
                GenerationId = ModelGenerationIds.CobaltGeneration
            },
            new Vehicle
            {
                Id = VehicleIds.CobaltVehicle2,
                LicensePlate = "A071BP",
                Color = "White",
                GenerationId = ModelGenerationIds.CobaltGeneration
            },
            new Vehicle
            {
                Id = VehicleIds.CamryVehicle,
                LicensePlate = "T8019X",
                Color = "Silver",
                GenerationId = ModelGenerationIds.CamryGeneration
            },
            new Vehicle
            {
                Id = VehicleIds.BmwX5Vehicle,
                LicensePlate = "Р307НТ",
                Color = "Blue",
                GenerationId = ModelGenerationIds.BmwX5Generation
            },
            new Vehicle
            {
                Id = VehicleIds.FordTransitVehicle,
                LicensePlate = "T210АВ",
                Color = "White",
                GenerationId = ModelGenerationIds.FordTransitGeneration
            },
            new Vehicle
            {
                Id = VehicleIds.TeslaVehicle,
                LicensePlate = "В801АН",
                Color = "Red",
                GenerationId = ModelGenerationIds.TeslaGeneration
            }
        };

        await context.Vehicles.AddRangeAsync(vehicles);
    }

    private static async Task SeedRentersAsync(CarRentalDbContext context)
    {
        var renters = new[]
        {
            new Renter
            {
                Id = RenterIds.Renter1,
                LicenseNumber = "1234123412",
                FullName = "Алексей Петров",
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Renter
            {
                Id = RenterIds.Renter2,
                LicenseNumber = "3456345634",
                FullName = "Екатерина Новикова",
                DateOfBirth = new DateTime(1985, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Renter
            {
                Id = RenterIds.Renter3,
                LicenseNumber = "5678567856",
                FullName = "Дмитрий Козлов",
                DateOfBirth = new DateTime(1995, 5, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Renter
            {
                Id = RenterIds.Renter4,
                LicenseNumber = "7890789078",
                FullName = "Мария Смирнова",
                DateOfBirth = new DateTime(1988, 8, 22, 0, 0, 0, DateTimeKind.Utc)
            },
            new Renter
            {
                Id = RenterIds.Renter5,
                LicenseNumber = "9012901290",
                FullName = "Иван Сидоров",
                DateOfBirth = new DateTime(1992, 11, 30, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        await context.Renters.AddRangeAsync(renters);
    }

    private static async Task SeedRentalsAsync(CarRentalDbContext context)
    {
        var now = DateTime.UtcNow;
        var rentals = new[]
        {
            new Rental
            {
                Id = RentalIds.Rental1,
                RentStartTime = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                DurationHours = 5,
                TotalCost = 1200.00m * 5,
                VehicleId = VehicleIds.CobaltVehicle1,
                RenterId = RenterIds.Renter1
            },
            new Rental
            {
                Id = RentalIds.Rental2,
                RentStartTime = new DateTime(2024, 1, 2, 14, 0, 0, DateTimeKind.Utc),
                DurationHours = 3,
                TotalCost = 1200.00m * 3,
                VehicleId = VehicleIds.CobaltVehicle1,
                RenterId = RenterIds.Renter2
            },
            new Rental
            {
                Id = RentalIds.Rental3,
                RentStartTime = new DateTime(2024, 1, 3, 9, 0, 0, DateTimeKind.Utc),
                DurationHours = 8,
                TotalCost = 1200.00m * 8,
                VehicleId = VehicleIds.CobaltVehicle2,
                RenterId = RenterIds.Renter1
            },
            new Rental
            {
                Id = RentalIds.Rental4,
                RentStartTime = new DateTime(2024, 1, 4, 11, 0, 0, DateTimeKind.Utc),
                DurationHours = 6,
                TotalCost = 1500.00m * 6,
                VehicleId = VehicleIds.CamryVehicle,
                RenterId = RenterIds.Renter2
            },
            new Rental
            {
                Id = RentalIds.Rental5,
                RentStartTime = new DateTime(2024, 1, 5, 13, 0, 0, DateTimeKind.Utc),
                DurationHours = 24,
                TotalCost = 3500.00m * 24,
                VehicleId = VehicleIds.BmwX5Vehicle,
                RenterId = RenterIds.Renter3
            },
            new Rental
            {
                Id = RentalIds.Rental6,
                RentStartTime = new DateTime(2024, 1, 6, 8, 0, 0, DateTimeKind.Utc),
                DurationHours = 12,
                TotalCost = 1800.00m * 12,
                VehicleId = VehicleIds.FordTransitVehicle,
                RenterId = RenterIds.Renter4
            },
            new Rental
            {
                Id = RentalIds.Rental7,
                RentStartTime = new DateTime(2024, 1, 7, 16, 0, 0, DateTimeKind.Utc),
                DurationHours = 2,
                TotalCost = 2800.00m * 2,
                VehicleId = VehicleIds.TeslaVehicle,
                RenterId = RenterIds.Renter5
            },
            new Rental
            {
                Id = RentalIds.Rental8,
                RentStartTime = new DateTime(2024, 1, 8, 10, 0, 0, DateTimeKind.Utc),
                DurationHours = 4,
                TotalCost = 1500.00m * 4,
                VehicleId = VehicleIds.CamryVehicle,
                RenterId = RenterIds.Renter3
            },
            new Rental
            {
                Id = RentalIds.Rental9,
                RentStartTime = new DateTime(2024, 1, 9, 15, 0, 0, DateTimeKind.Utc),
                DurationHours = 7,
                TotalCost = 1200.00m * 7,
                VehicleId = VehicleIds.CobaltVehicle1,
                RenterId = RenterIds.Renter4
            },
            new Rental
            {
                Id = RentalIds.CurrentRental1,
                RentStartTime = now.AddHours(-2),
                DurationHours = 5,
                TotalCost = 1200.00m * 5,
                VehicleId = VehicleIds.CobaltVehicle2,
                RenterId = RenterIds.Renter1
            },
            new Rental
            {
                Id = RentalIds.CurrentRental2,
                RentStartTime = now.AddHours(-1),
                DurationHours = 7,
                TotalCost = 3500.00m * 3,
                VehicleId = VehicleIds.BmwX5Vehicle,
                RenterId = RenterIds.Renter5
            }
        };

        await context.Rentals.AddRangeAsync(rentals);
    }
}