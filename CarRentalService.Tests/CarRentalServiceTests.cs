using CarRentalService.Domain.Domain;
using CarRentalService.Tests.Fixture;

namespace CarRentalService.Tests;

/// <summary>
/// Unit tests for CarRentalService rental operations and reporting
/// </summary>
public class RentalServiceTests : IClassFixture<CarRentalFixture>
{
    private readonly CarRentalFixture _fixture;

    /// <summary>
    /// Initializes a new instance with the provided test data fixture
    /// </summary>
    /// <param name="fixture">The test data fixture containing rental service data</param>
    public RentalServiceTests(CarRentalFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Retrieves information about all customers who rented cars of the specified model, ordered by full name
    /// </summary>
    [Fact]
    public void GetCustomersRentingModel_OrderedByName()
    {
        var targetModel = "Chevrolet Cobalt";
        var result = _fixture.Rentals
            .Where(r => r.Car.Generation.Model.Name == targetModel)
            .Select(r => r.Renter)
            .Distinct()
            .OrderBy(r => r.FullName)
            .ToList();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Андрей Петров", result[0].FullName);     
        Assert.Equal("Екатерина Новикова", result[1].FullName);
    }

    /// <summary>
    /// Retrieves information about vehicles currently under rental
    /// </summary>
    [Fact]
    public void GetVehiclesCurrentlyRented()
    {
        var testTime = new DateTime(2024, 1, 1, 12, 0, 0);
        var result = _fixture.Rentals
            .Where(r => r.RentStartTime <= testTime &&
                       r.RentStartTime.AddHours(r.DurationHours) >= testTime)
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Н099ОР", result[0].LicensePlate);
        Assert.Equal("Chevrolet Cobalt", result[0].Generation.Model.Name);
    }

    /// <summary>
    /// Retrieves the top 5 most frequently rented vehicles
    /// </summary>
    [Fact]
    public void GetTop5MostFrequentlyRentedVehicles()
    {
        var result = _fixture.Rentals
            .GroupBy(r => r.Car)
            .Select(g => new { Vehicle = g.Key, RentalCount = g.Count() })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        var topVehicle = result[0];
        Assert.Equal(2, topVehicle.RentalCount);
        Assert.Equal("Н099ОР", topVehicle.Vehicle.LicensePlate);
        Assert.Equal(1, result[1].RentalCount);
        Assert.Equal(1, result[2].RentalCount);
    }

    /// <summary>
    /// Retrieves rental count for each vehicle
    /// </summary>
    [Fact]
    public void GetRentalCountPerVehicle()
    {
        var result = _fixture.Vehicles
            .Select(vehicle => new
            {
                Vehicle = vehicle,
                RentalCount = _fixture.Rentals.Count(r => r.Car.Id == vehicle.Id)
            })
            .ToList();

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        var cobalt1Result = result.First(x => x.Vehicle.LicensePlate == "Н099ОР");
        var cobalt2Result = result.First(x => x.Vehicle.LicensePlate == "А071ВР");
        var camryResult = result.First(x => x.Vehicle.LicensePlate == "Т801УХ");
        Assert.Equal(2, cobalt1Result.RentalCount);
        Assert.Equal(1, cobalt2Result.RentalCount);
        Assert.Equal(1, camryResult.RentalCount);
    }

    /// <summary>
    /// Retrieves the top 5 customers by total rental cost
    /// </summary>
    [Fact]
    public void GetTop5CustomersByRentalCost()
    {
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
        Assert.Equal(15600, topCustomer.TotalCost);
        Assert.Equal("Андрей Петров", topCustomer.Customer.FullName);
        var secondCustomer = result[1];
        Assert.Equal(12600, secondCustomer.TotalCost);
        Assert.Equal("Екатерина Новикова", secondCustomer.Customer.FullName);
    }
}