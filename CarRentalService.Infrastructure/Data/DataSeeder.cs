using CarRentalService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Data;

/// <summary>
/// Provides seed data for the CarRentalService database
/// </summary>
public static class DataSeeder
{
    private static readonly Guid _chevroletCobaltModelId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid _toyotaCamryModelId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid _bmwX5ModelId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly Guid _fordTransitModelId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid _teslaModel3Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

    private static readonly Guid _cobaltGenerationId = Guid.Parse("11111111-2222-2222-2222-222222222222");
    private static readonly Guid _camryGenerationId = Guid.Parse("22222222-3333-3333-3333-333333333333");
    private static readonly Guid _bmwX5GenerationId = Guid.Parse("33333333-4444-4444-4444-444444444444");
    private static readonly Guid _fordTransitGenerationId = Guid.Parse("44444444-5555-5555-5555-555555555555");
    private static readonly Guid _teslaGenerationId = Guid.Parse("55555555-6666-6666-6666-666666666666");

    private static readonly Guid _cobaltVehicle1Id = Guid.Parse("11111111-7777-7777-7777-777777777777");
    private static readonly Guid _cobaltVehicle2Id = Guid.Parse("22222222-8888-8888-8888-888888888888");
    private static readonly Guid _camryVehicleId = Guid.Parse("33333333-9999-9999-9999-999999999999");
    private static readonly Guid _bmwX5VehicleId = Guid.Parse("44444444-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
    private static readonly Guid _fordTransitVehicleId = Guid.Parse("55555555-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
    private static readonly Guid _teslaVehicleId = Guid.Parse("66666666-CCCC-CCCC-CCCC-CCCCCCCCCCCC");

    private static readonly Guid _renter1Id = Guid.Parse("77777777-1111-1111-1111-111111111111");
    private static readonly Guid _renter2Id = Guid.Parse("88888888-2222-2222-2222-222222222222");
    private static readonly Guid _renter3Id = Guid.Parse("99999999-3333-3333-3333-333333333333");
    private static readonly Guid _renter4Id = Guid.Parse("AAAAAAAA-4444-4444-4444-444444444444");
    private static readonly Guid _renter5Id = Guid.Parse("BBBBBBBB-5555-5555-5555-555555555555");

    private static readonly Guid _rental1Id = Guid.Parse("CCCCCCCC-1111-1111-1111-111111111111");
    private static readonly Guid _rental2Id = Guid.Parse("DDDDDDDD-2222-2222-2222-222222222222");
    private static readonly Guid _rental3Id = Guid.Parse("EEEEEEEE-3333-3333-3333-333333333333");
    private static readonly Guid _rental4Id = Guid.Parse("FFFFFFFF-4444-4444-4444-444444444444");
    private static readonly Guid _rental5Id = Guid.Parse("11111111-5555-5555-5555-555555555555");
    private static readonly Guid _rental6Id = Guid.Parse("22222222-6666-6666-6666-666666666666");
    private static readonly Guid _rental7Id = Guid.Parse("33333333-7777-7777-7777-777777777777");
    private static readonly Guid _rental8Id = Guid.Parse("44444444-8888-8888-8888-888888888888");
    private static readonly Guid _rental9Id = Guid.Parse("55555555-9999-9999-9999-999999999999");
    private static readonly Guid _currentRental1Id = Guid.Parse("66666666-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
    private static readonly Guid _currentRental2Id = Guid.Parse("77777777-BBBB-BBBB-BBBB-BBBBBBBBBBBB");

    /// <summary>
    /// Seeds the database with initial test data
    /// </summary>
    /// <param name="context">The database context</param>
    public static async Task SeedAsync(CarRentalDbContext context)
    {
        try
        {
            if (await context.VehicleModels.AnyAsync())
                return;

            var vehicleModels = new[]
            {
                new VehicleModel
                {
                    Id = _chevroletCobaltModelId,
                    Name = "Chevrolet Cobalt",
                    DriveType = Domain.DriveType.Fwd,
                    SeatCount = 5,
                    BodyType = BodyType.Sedan,
                    VehicleClass = VehicleClass.Compact
                },
                new VehicleModel
                {
                    Id = _toyotaCamryModelId,
                    Name = "Toyota Camry",
                    DriveType = Domain.DriveType.Fwd,
                    SeatCount = 5,
                    BodyType = BodyType.Sedan,
                    VehicleClass = VehicleClass.Family
                },
                new VehicleModel
                {
                    Id = _bmwX5ModelId,
                    Name = "BMW X5",
                    DriveType = Domain.DriveType.Awd,
                    SeatCount = 7,
                    BodyType = BodyType.Suv,
                    VehicleClass = VehicleClass.Luxury
                },
                new VehicleModel
                {
                    Id = _fordTransitModelId,
                    Name = "Ford Transit",
                    DriveType = Domain.DriveType.Rwd,
                    SeatCount = 9,
                    BodyType = BodyType.Minivan,
                    VehicleClass = VehicleClass.Family
                },
                new VehicleModel
                {
                    Id = _teslaModel3Id,
                    Name = "Tesla Model 3",
                    DriveType = Domain.DriveType.Awd,
                    SeatCount = 5,
                    BodyType = BodyType.Sedan,
                    VehicleClass = VehicleClass.Luxury
                }
            };

            await context.VehicleModels.AddRangeAsync(vehicleModels);

            var modelGenerations = new[]
            {
                new ModelGeneration
                {
                    Id = _cobaltGenerationId,
                    Year = 2016,
                    EngineVolume = 1.8,
                    Transmission = Transmission.Automatic,
                    RentalPricePerHour = 1200.00m,
                    VehicleModelId = _chevroletCobaltModelId
                },
                new ModelGeneration
                {
                    Id = _camryGenerationId,
                    Year = 2023,
                    EngineVolume = 2.0,
                    Transmission = Transmission.Automatic,
                    RentalPricePerHour = 1500.00m,
                    VehicleModelId = _toyotaCamryModelId
                },
                new ModelGeneration
                {
                    Id = _bmwX5GenerationId,
                    Year = 2024,
                    EngineVolume = 3.0,
                    Transmission = Transmission.Automatic,
                    RentalPricePerHour = 3500.00m,
                    VehicleModelId = _bmwX5ModelId
                },
                new ModelGeneration
                {
                    Id = _fordTransitGenerationId,
                    Year = 2022,
                    EngineVolume = 2.2,
                    Transmission = Transmission.Manual,
                    RentalPricePerHour = 1800.00m,
                    VehicleModelId = _fordTransitModelId
                },
                new ModelGeneration
                {
                    Id = _teslaGenerationId,
                    Year = 2023,
                    EngineVolume = 0,
                    Transmission = Transmission.Automatic,
                    RentalPricePerHour = 2800.00m,
                    VehicleModelId = _teslaModel3Id
                }
            };

            await context.ModelGenerations.AddRangeAsync(modelGenerations);

            var vehicles = new[]
            {
                new Vehicle
                {
                    Id = _cobaltVehicle1Id,
                    LicensePlate = "H0990P",
                    Color = "Black",
                    GenerationId = _cobaltGenerationId
                },
                new Vehicle
                {
                    Id = _cobaltVehicle2Id,
                    LicensePlate = "A071BP",
                    Color = "White",
                    GenerationId = _cobaltGenerationId
                },
                new Vehicle
                {
                    Id = _camryVehicleId,
                    LicensePlate = "T8019X",
                    Color = "Silver",
                    GenerationId = _camryGenerationId
                },
                new Vehicle
                {
                    Id = _bmwX5VehicleId,
                    LicensePlate = "Р307НТ",
                    Color = "Blue",
                    GenerationId = _bmwX5GenerationId
                },
                new Vehicle
                {
                    Id = _fordTransitVehicleId,
                    LicensePlate = "T210АВ",
                    Color = "White",
                    GenerationId = _fordTransitGenerationId
                },
                new Vehicle
                {
                    Id = _teslaVehicleId,
                    LicensePlate = "В801АН",
                    Color = "Red",
                    GenerationId = _teslaGenerationId
                }
            };

            await context.Vehicles.AddRangeAsync(vehicles);

            var renters = new[]
            {
                new Renter
                {
                    Id = _renter1Id,
                    LicenseNumber = "1234123412",
                    FullName = "Алексей Петров",
                    DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Renter
                {
                    Id = _renter2Id,
                    LicenseNumber = "3456345634",
                    FullName = "Екатерина Новикова",
                    DateOfBirth = new DateTime(1985, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Renter
                {
                    Id = _renter3Id,
                    LicenseNumber = "5678567856",
                    FullName = "Дмитрий Козлов",
                    DateOfBirth = new DateTime(1995, 5, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Renter
                {
                    Id = _renter4Id,
                    LicenseNumber = "7890789078",
                    FullName = "Мария Смирнова",
                    DateOfBirth = new DateTime(1988, 8, 22, 0, 0, 0, DateTimeKind.Utc)
                },
                new Renter
                {
                    Id = _renter5Id,
                    LicenseNumber = "9012901290",
                    FullName = "Иван Сидоров",
                    DateOfBirth = new DateTime(1992, 11, 30, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            await context.Renters.AddRangeAsync(renters);

            var now = DateTime.UtcNow;
            var rentals = new[]
            {
                new Rental
                {
                    Id = _rental1Id,
                    RentStartTime = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    DurationHours = 5,
                    TotalCost = 1200.00m * 5,
                    VehicleId = _cobaltVehicle1Id,
                    RenterId = _renter1Id
                },
                new Rental
                {
                    Id = _rental2Id,
                    RentStartTime = new DateTime(2024, 1, 2, 14, 0, 0, DateTimeKind.Utc),
                    DurationHours = 3,
                    TotalCost = 1200.00m * 3,
                    VehicleId = _cobaltVehicle1Id,
                    RenterId = _renter2Id
                },
                new Rental
                {
                    Id = _rental3Id,
                    RentStartTime = new DateTime(2024, 1, 3, 9, 0, 0, DateTimeKind.Utc),
                    DurationHours = 8,
                    TotalCost = 1200.00m * 8,
                    VehicleId = _cobaltVehicle2Id,
                    RenterId = _renter1Id
                },
                new Rental
                {
                    Id = _rental4Id,
                    RentStartTime = new DateTime(2024, 1, 4, 11, 0, 0, DateTimeKind.Utc),
                    DurationHours = 6,
                    TotalCost = 1500.00m * 6,
                    VehicleId = _camryVehicleId,
                    RenterId = _renter2Id
                },
                new Rental
                {
                    Id = _rental5Id,
                    RentStartTime = new DateTime(2024, 1, 5, 13, 0, 0, DateTimeKind.Utc),
                    DurationHours = 24,
                    TotalCost = 3500.00m * 24,
                    VehicleId = _bmwX5VehicleId,
                    RenterId = _renter3Id
                },
                new Rental
                {
                    Id = _rental6Id,
                    RentStartTime = new DateTime(2024, 1, 6, 8, 0, 0, DateTimeKind.Utc),
                    DurationHours = 12,
                    TotalCost = 1800.00m * 12,
                    VehicleId = _fordTransitVehicleId,
                    RenterId = _renter4Id
                },
                new Rental
                {
                    Id = _rental7Id,
                    RentStartTime = new DateTime(2024, 1, 7, 16, 0, 0, DateTimeKind.Utc),
                    DurationHours = 2,
                    TotalCost = 2800.00m * 2,
                    VehicleId = _teslaVehicleId,
                    RenterId = _renter5Id
                },
                new Rental
                {
                    Id = _rental8Id,
                    RentStartTime = new DateTime(2024, 1, 8, 10, 0, 0, DateTimeKind.Utc),
                    DurationHours = 4,
                    TotalCost = 1500.00m * 4,
                    VehicleId = _camryVehicleId,
                    RenterId = _renter3Id
                },
                new Rental
                {
                    Id = _rental9Id,
                    RentStartTime = new DateTime(2024, 1, 9, 15, 0, 0, DateTimeKind.Utc),
                    DurationHours = 7,
                    TotalCost = 1200.00m * 7,
                    VehicleId = _cobaltVehicle1Id,
                    RenterId = _renter4Id
                },
                
                new Rental
                {
                    Id = _currentRental1Id,
                    RentStartTime = now.AddHours(-2),
                    DurationHours = 5,
                    TotalCost = 1200.00m * 5,
                    VehicleId = _cobaltVehicle2Id,
                    RenterId = _renter1Id
                },
                new Rental
                {
                    Id = _currentRental2Id,
                    RentStartTime = now.AddHours(-1),
                    DurationHours = 7,
                    TotalCost = 3500.00m * 3,
                    VehicleId = _bmwX5VehicleId,
                    RenterId = _renter5Id
                }
            };

            await context.Rentals.AddRangeAsync(rentals);

            await context.SaveChangesAsync();

            Console.WriteLine("Database seeded with diverse test data!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding database: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Gets the Chevrolet Cobalt model identifier for testing purposes
    /// </summary>
    /// <returns>The Chevrolet Cobalt model identifier</returns>
    public static Guid GetChevroletCobaltModelId() => _chevroletCobaltModelId;

    /// <summary>
    /// Gets the Toyota Camry model identifier for testing purposes
    /// </summary>
    /// <returns>The Toyota Camry model identifier</returns>
    public static Guid GetToyotaCamryModelId() => _toyotaCamryModelId;

    /// <summary>
    /// Gets the first Cobalt vehicle identifier for testing purposes
    /// </summary>
    /// <returns>The Cobalt vehicle identifier</returns>
    public static Guid GetCobaltVehicle1Id() => _cobaltVehicle1Id;

    /// <summary>
    /// Gets the first renter identifier for testing purposes
    /// </summary>
    /// <returns>The renter identifier</returns>
    public static Guid GetRenter1Id() => _renter1Id;

    /// <summary>
    /// Gets the BMW X5 vehicle identifier for testing purposes
    /// </summary>
    /// <returns>The BMW X5 vehicle identifier</returns>
    public static Guid GetBmwX5VehicleId() => _bmwX5VehicleId;

    /// <summary>
    /// Gets the Tesla vehicle identifier for testing purposes
    /// </summary>
    /// <returns>The Tesla vehicle identifier</returns>
    public static Guid GetTeslaVehicleId() => _teslaVehicleId;
}