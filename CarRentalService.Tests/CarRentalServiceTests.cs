using CarRentalService.Tests.Fixture;

namespace CarRentalService.Tests;

/// <summary>
/// Unit tests for CarRentalService rental operations and reporting
/// </summary>
public class RentalServiceTests(CarRentalFixture _fixture) : IClassFixture<CarRentalFixture>
{
    private const string ChevroletCobaltModelName = "Chevrolet Cobalt";
    private const string ToyotaCamryModelName = "Toyota Camry";
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
        var targetModel = ChevroletCobaltModelName;
        var expectedCustomer1 = AndreyPetrovFullName;
        var expectedCustomer2 = EkaterinaNovikovaFullName;

        var result = _fixture.Rentals
            .Where(r => r.Car.Generation.Model.Name == targetModel)
            .Select(r => r.Renter)
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
        var expectedModelName = ChevroletCobaltModelName;

        var result = _fixture.Rentals
            .Where(r => r.RentStartTime <= testTime &&
                       r.RentStartTime.AddHours(r.DurationHours) >= testTime)
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(expectedLicensePlate, result[0].LicensePlate);
        Assert.Equal(expectedModelName, result[0].Generation.Model.Name);
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
            .GroupBy(r => r.Car)
            .Select(g => new { Vehicle = g.Key, RentalCount = g.Count() })
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
                RentalCount = _fixture.Rentals.Count(r => r.Car.Id == vehicle.Id)
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
        var expectedTopCustomerCost = 15600;
        var expectedSecondCustomerName = EkaterinaNovikovaFullName;
        var expectedSecondCustomerCost = 12600;

        var result = _fixture.Rentals
            .GroupBy(r => r.Renter)
            .Select(g => new
            {
                Customer = g.Key,
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