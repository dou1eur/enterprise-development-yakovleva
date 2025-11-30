using CarRentalService.Tests.Fixture;

namespace CarRentalService.Tests;

/// <summary>
/// Unit tests for CarRentalService rental operations and reporting
/// </summary>
public class CarRentalServiceTests(CarRentalFixture _fixture) : IClassFixture<CarRentalFixture>
{
    private const string ChevroletCobaltModelName = "Chevrolet Cobalt";
    private const string AndreyPetrovFullName = "Андрей Петров";
    private const string EkaterinaNovikovaFullName = "Екатерина Новикова";
    private const string LicensePlateCobalt1 = "Н099ОР";
    private const string LicensePlateCobalt2 = "А071ВР";
    private const string LicensePlateCamry = "Т801УХ";

    /// <summary>
    /// Retrieves information about all customers who rented cars of the specified model, ordered by full name
    /// </summary>
    [Fact]
    public void GetCustomersRentingModelOrderedByName()
    {
        var targetModel = _fixture.Models.First(m => m.Name == ChevroletCobaltModelName);
        var expectedCustomer1 = AndreyPetrovFullName;
        var expectedCustomer2 = EkaterinaNovikovaFullName;

        var cobaltGenerationIds = _fixture.Generations
            .Where(g => g.VehicleModelId == targetModel.Id)
            .Select(g => g.Id)
            .ToList();

        var cobaltVehicleIds = _fixture.Vehicles
            .Where(v => cobaltGenerationIds.Contains(v.GenerationId))
            .Select(v => v.Id)
            .ToList();

        var result = _fixture.Rentals
            .Where(r => cobaltVehicleIds.Contains(r.VehicleId))
            .Select(r => _fixture.GetRenterById(r.RenterId))
            .Distinct()
            .OrderBy(r => r.FullName)
            .ToList();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(expectedCustomer1, result[0].FullName);
        Assert.Equal(expectedCustomer2, result[1].FullName);
    }

    /// <summary>
    /// Retrieves information about vehicles currently under rental
    /// </summary>
    [Fact]
    public void GetVehiclesCurrentlyRented()
    {
        var testTime = new DateTime(2024, 1, 1, 12, 0, 0);
        var expectedLicensePlate = LicensePlateCobalt1;

        var result = _fixture.Rentals
            .Where(r => r.RentStartTime <= testTime &&
                       r.RentStartTime.AddHours(r.DurationHours) >= testTime)
            .Select(r => _fixture.GetVehicleById(r.VehicleId))
            .Distinct()
            .ToList();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(expectedLicensePlate, result[0].LicensePlate);
    }

    /// <summary>
    /// Retrieves the top 5 most frequently rented vehicles
    /// </summary>
    [Fact]
    public void GetTop5MostFrequentlyRentedVehicles()
    {
        var expectedTopLicensePlate = LicensePlateCobalt1;
        var expectedTopRentalCount = 2;

        var result = _fixture.Rentals
            .GroupBy(r => r.VehicleId)
            .Select(g => new
            {
                Vehicle = _fixture.GetVehicleById(g.Key),
                RentalCount = g.Count()
            })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);

        var topVehicle = result[0];
        Assert.Equal(expectedTopRentalCount, topVehicle.RentalCount);
        Assert.Equal(expectedTopLicensePlate, topVehicle.Vehicle.LicensePlate);

        Assert.Equal(1, result[1].RentalCount);
        Assert.Equal(1, result[2].RentalCount);
    }

    /// <summary>
    /// Retrieves rental count for each vehicle
    /// </summary>
    [Fact]
    public void GetRentalCountPerVehicle()
    {
        var expectedCobalt1LicensePlate = LicensePlateCobalt1;
        var expectedCobalt1RentalCount = 2;
        var expectedCobalt2LicensePlate = LicensePlateCobalt2;
        var expectedCobalt2RentalCount = 1;
        var expectedCamryLicensePlate = LicensePlateCamry;
        var expectedCamryRentalCount = 1;
        var expectedTotalVehicles = 3;

        var result = _fixture.Vehicles
            .Select(vehicle => new
            {
                Vehicle = vehicle,
                RentalCount = _fixture.Rentals.Count(r => r.VehicleId == vehicle.Id)
            })
            .ToList();

        Assert.NotNull(result);
        Assert.Equal(expectedTotalVehicles, result.Count);

        var cobalt1Result = result.First(x => x.Vehicle.LicensePlate == expectedCobalt1LicensePlate);
        var cobalt2Result = result.First(x => x.Vehicle.LicensePlate == expectedCobalt2LicensePlate);
        var camryResult = result.First(x => x.Vehicle.LicensePlate == expectedCamryLicensePlate);

        Assert.Equal(expectedCobalt1RentalCount, cobalt1Result.RentalCount);
        Assert.Equal(expectedCobalt2RentalCount, cobalt2Result.RentalCount);
        Assert.Equal(expectedCamryRentalCount, camryResult.RentalCount);
    }

    /// <summary>
    /// Retrieves the top 5 customers by total rental cost
    /// </summary>
    [Fact]
    public void GetTop5CustomersByRentalCost()
    {
        var expectedTopCustomerName = AndreyPetrovFullName;
        var expectedTopCustomerCost = 15600m;
        var expectedSecondCustomerName = EkaterinaNovikovaFullName;
        var expectedSecondCustomerCost = 12600m;

        var result = _fixture.Rentals
            .GroupBy(r => r.RenterId)
            .Select(g => new
            {
                Customer = _fixture.GetRenterById(g.Key),
                TotalCost = g.Sum(r => r.TotalCost)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .ToList();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        var topCustomer = result[0];
        Assert.Equal(expectedTopCustomerCost, topCustomer.TotalCost);
        Assert.Equal(expectedTopCustomerName, topCustomer.Customer.FullName);

        var secondCustomer = result[1];
        Assert.Equal(expectedSecondCustomerCost, secondCustomer.TotalCost);
        Assert.Equal(expectedSecondCustomerName, secondCustomer.Customer.FullName);
    }
}