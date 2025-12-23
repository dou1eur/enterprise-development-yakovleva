using Bogus;
using CarRentalService.Application.Contracts.Rental;
using CarRentalService.TestData.Constants;
namespace CarRentalService.Generator.Kafka.Services;

public static class RentalDataGenerator
{
    /// <summary>
    /// Generates a list of test rental contracts using real IDs
    /// </summary>
    public static List<RentalRequest> GenerateRentals(int count)
    {
        var faker = new Faker<RentalRequest>()
            .CustomInstantiator(f => new RentalRequest(
                RentStartTime: f.Date.Future().ToUniversalTime(),
                RentalDurationHours: f.Random.Int(1, 72),
                VehicleId: f.PickRandom(VehicleIds.All),
                RenterId: f.PickRandom(RenterIds.All)
            ));

        return faker.Generate(count);
    }
}