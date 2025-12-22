using Bogus;
using CarRentalService.Application.Contracts.Rental;

namespace CarRentalService.Generator.Kafka.Services;

/// <summary>
/// Test data generator for rental contracts
/// </summary>
public static class RentalDataGenerator
{
    /// <summary>
    /// Generates a list of test rental contracts
    /// </summary>
    /// <param name="count">Number of rentals to generate</param>
    /// <returns>Generated list of rental requests</returns>
    public static List<RentalRequest> GenerateRentals(int count)
    {
        var faker = new Faker<RentalRequest>()
            .CustomInstantiator(f => new RentalRequest(
                RentStartTime: f.Date.Future(),
                RentalDurationHours: f.Random.Int(1, 72),
                VehicleId: f.Random.Guid(),
                RenterId: f.Random.Guid()
            ));

        return faker.Generate(count);
    }
}