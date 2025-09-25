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
    /// Initializes a new instance of RentalServiceTests with the provided test data fixture
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
        var targetModel = "Honda Civic";
        var result = _fixture.Rentals
            .Where(r => r.Car.Generation.Model.Name.Contains(targetModel))
            .Select(r => r.Renter)
            .Distinct()
            .OrderBy(r => r.FullName)
            .ToList();

        Assert.NotNull(result);
        Assert.All(result, renter =>
            Assert.Contains(_fixture.Rentals, r =>
                r.Renter.Id == renter.Id &&
                r.Car.Generation.Model.Name.Contains(targetModel)));

        var sortedNames = result.Select(r => r.FullName).OrderBy(name => name).ToList();
        Assert.Equal(sortedNames, result.Select(r => r.FullName).ToList());
    }

    /// <summary>
    /// Retrieves information about vehicles currently under rental
    /// </summary>
    [Fact]
    public void GetVehiclesCurrentlyRented()
    {
        var now = DateTime.Now;
        var result = _fixture.Rentals
            .Where(r => r.RentStartTime <= now &&
                       r.RentStartTime.AddHours(r.DurationHours) >= now)
            .Select(r => r.Car)
            .Distinct()
            .ToList();

        Assert.NotNull(result);
        Assert.All(result, vehicle =>
            Assert.Contains(_fixture.Rentals, r =>
                r.Car.Id == vehicle.Id &&
                r.RentStartTime <= now &&
                r.RentStartTime.AddHours(r.DurationHours) >= now));
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
        Assert.InRange(result.Count, 0, 5);
        if (result.Count > 1)
        {
            for (var i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].RentalCount >= result[i + 1].RentalCount);
            }
        }

        var allRentalCounts = _fixture.Rentals
            .GroupBy(r => r.Car)
            .Select(g => g.Count())
            .OrderByDescending(c => c)
            .Take(5)
            .ToList();

        if (allRentalCounts.Any())
        {
            Assert.Equal(allRentalCounts, result.Select(x => x.RentalCount).ToList());
        }
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
        Assert.Equal(_fixture.Vehicles.Count, result.Count);
        foreach (var item in result)
        {
            var expectedCount = _fixture.Rentals.Count(r => r.Car.Id == item.Vehicle.Id);
            Assert.Equal(expectedCount, item.RentalCount);
        }
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
        Assert.InRange(result.Count, 0, 5);
        if (result.Count > 1)
        {
            for (var i = 0; i < result.Count - 1; i++)
            {
                Assert.True(result[i].TotalCost >= result[i + 1].TotalCost);
            }
        }
        foreach (var item in result)
        {
            var expectedSum = _fixture.Rentals
                .Where(r => r.Renter.Id == item.Customer.Id)
                .Sum(r => r.TotalCost);
            Assert.Equal(expectedSum, item.TotalCost);
        }
    }
}