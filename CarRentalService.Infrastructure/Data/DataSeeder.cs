using CarRentalService.Domain;
using CarRentalService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentalService.Infrastructure.Data;

public static class DataSeeder
{
    private static readonly Guid _chevroletCobaltModelId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid _toyotaCamryModelId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid _cobaltGenerationId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    private static readonly Guid _camryGenerationId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    private static readonly Guid _cobaltVehicle1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
    private static readonly Guid _cobaltVehicle2Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
    private static readonly Guid _camryVehicleId = Guid.Parse("77777777-7777-7777-7777-777777777777");
    private static readonly Guid _renter1Id = Guid.Parse("88888888-8888-8888-8888-888888888888");
    private static readonly Guid _renter2Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
    private static readonly Guid _rental1Id = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
    private static readonly Guid _rental2Id = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
    private static readonly Guid _rental3Id = Guid.Parse("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC");
    private static readonly Guid _rental4Id = Guid.Parse("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD");

    public static async Task SeedAsync(CarRentalDbContext context)
    {
        try
        {

            if (await context.VehicleModels.AnyAsync())
                return;

            var chevroletCobaltModel = new VehicleModelEntity
            {
                Id = _chevroletCobaltModelId,
                Name = "Chevrolet Cobalt",
                DriveType = Domain.DriveType.Fwd,
                SeatCount = 5,
                BodyType = BodyType.Sedan,
                VehicleClass = VehicleClass.Compact
            };

            var toyotaCamryModel = new VehicleModelEntity
            {
                Id = _toyotaCamryModelId,
                Name = "Toyota Camry",
                DriveType = Domain.DriveType.Fwd,
                SeatCount = 5,
                BodyType = BodyType.Sedan,
                VehicleClass = VehicleClass.Family
            };

            await context.VehicleModels.AddRangeAsync(chevroletCobaltModel, toyotaCamryModel);

            var cobaltGeneration = new ModelGenerationEntity
            {
                Id = _cobaltGenerationId,
                Year = 2016,
                EngineVolume = 1.8,
                Transmission = Transmission.Automatic,
                RentalPricePerHour = 1200.00m,
                VehicleModelId = chevroletCobaltModel.Id
            };

            var camryGeneration = new ModelGenerationEntity
            {
                Id = _camryGenerationId,
                Year = 2023,
                EngineVolume = 2.0,
                Transmission = Transmission.Automatic,
                RentalPricePerHour = 1500.00m,
                VehicleModelId = toyotaCamryModel.Id
            };

            await context.ModelGenerations.AddRangeAsync(cobaltGeneration, camryGeneration);

            var cobaltVehicle1 = new VehicleEntity
            {
                Id = _cobaltVehicle1Id,
                LicensePlate = "Н099ОР",
                Color = "Black",
                ModelGenerationId = cobaltGeneration.Id
            };

            var cobaltVehicle2 = new VehicleEntity
            {
                Id = _cobaltVehicle2Id,
                LicensePlate = "А071ВР",
                Color = "White",
                ModelGenerationId = cobaltGeneration.Id
            };

            var camryVehicle = new VehicleEntity
            {
                Id = _camryVehicleId,
                LicensePlate = "Т801УХ",
                Color = "Silver",
                ModelGenerationId = camryGeneration.Id
            };

            await context.Vehicles.AddRangeAsync(cobaltVehicle1, cobaltVehicle2, camryVehicle);

            var renter1 = new RenterEntity
            {
                Id = _renter1Id,
                LicenseNumber = "1234123412",
                FullName = "Андрей Петров",
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Local)
            };

            var renter2 = new RenterEntity
            {
                Id = _renter2Id,
                LicenseNumber = "3456345634",
                FullName = "Екатерина Новикова",
                DateOfBirth = new DateTime(1985, 1, 1, 0, 0, 0, DateTimeKind.Local)
            };

            await context.Renters.AddRangeAsync(renter1, renter2);

            var rental1 = new RentalEntity
            {
                Id = _rental1Id,
                RentStartTime = new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Local),
                DurationHours = 5,
                TotalCost = 1200.00m * 5,
                VehicleId = cobaltVehicle1.Id,
                RenterId = renter1.Id
            };

            var rental2 = new RentalEntity
            {
                Id = _rental2Id,
                RentStartTime = new DateTime(2024, 1, 2, 14, 0, 0, DateTimeKind.Local),
                DurationHours = 3,
                TotalCost = 1200.00m * 3,
                VehicleId = cobaltVehicle1.Id,
                RenterId = renter2.Id
            };

            var rental3 = new RentalEntity
            {
                Id = _rental3Id,
                RentStartTime = new DateTime(2024, 1, 3, 9, 0, 0, DateTimeKind.Local),
                DurationHours = 8,
                TotalCost = 1200.00m * 8,
                VehicleId = cobaltVehicle2.Id,
                RenterId = renter1.Id
            };

            var rental4 = new RentalEntity
            {
                Id = _rental4Id,
                RentStartTime = new DateTime(2024, 1, 4, 11, 0, 0, DateTimeKind.Local),
                DurationHours = 6,
                TotalCost = 1500.00m * 6,
                VehicleId = camryVehicle.Id,
                RenterId = renter2.Id
            };

            var currentRental = new RentalEntity
            {
                Id = Guid.NewGuid(),
                RentStartTime = DateTime.Now.AddHours(-2),
                DurationHours = 5,
                TotalCost = 1200.00m * 5,
                VehicleId = cobaltVehicle1.Id,
                RenterId = renter1.Id
            };

            await context.Rentals.AddRangeAsync(rental1, rental2, rental3, rental4, currentRental);

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding database: {ex.Message}");
            throw;
        }
    }
    public static Guid GetChevroletCobaltModelId() => _chevroletCobaltModelId;
    public static Guid GetToyotaCamryModelId() => _toyotaCamryModelId;
    public static Guid GetCobaltVehicle1Id() => _cobaltVehicle1Id;
    public static Guid GetRenter1Id() => _renter1Id;
}