using CarRentalService.Domain.Domain;

namespace CarRentalService.Domain.Fixture
/// <summary>
/// Provides test data for car rental service
/// </summary>
public class CarRentalFixture
{
    /// <summary>
    /// Collection of vehicle models
    /// </summary>
    public List<VehicleModel> Models { get; private set; } = new();

    /// <summary>
    /// Collection of vehicle generations
    /// </summary>
    public List<VehicleGeneration> Generations { get; private set; } = new();

    /// <summary>
    /// Collection of specific vehicles
    /// </summary>
    public List<Vehicle> Vehicles { get; private set; } = new();

    /// <summary>
    /// Collection of renters
    /// </summary>
    public List<Renter> Renters { get; private set; } = new();

    /// <summary>
    /// Collection of rental transactions
    /// </summary>
    public List<Rental> Rentals { get; private set; } = new();

    /// <summary>
    /// Initializes a new instance of the fixture and populates with test data
    /// </summary>
    public CarRentalFixture()
    {
        Initialize();
    }

    /// <summary>
    /// Initializes test data by clearing existing data and generating new data
    /// </summary>
    public void CarRentalFixture()
    {
        GenerateModels();
        GenerateGenerations();
        GenerateVehicles();
        GenerateRenters();
        GenerateRentals();
    }

    /// <summary>
    /// Generates test vehicle models
    /// </summary>
    private void GenerateModels()
    {
        Models.AddRange(new[]
        {
                new VehicleModel { Id = Guid.NewGuid(), Name = "Toyota Camry", DriveType = Domain.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, VehicleClass = VehicleClass.Family },
                new VehicleModel { Id = Guid.NewGuid(), Name = "BMW 5 Series", DriveType = Domain.DriveType.RWD, SeatCount = 5, BodyType = BodyType.Sedan, VehicleClass = VehicleClass.Business },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Mercedes E-Class", DriveType = Domain.DriveType.RWD, SeatCount = 5, BodyType = BodyType.Sedan, VehicleClass = VehicleClass.Luxury },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Honda Civic", DriveType = Domain.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Hatchback, VehicleClass = VehicleClass.Compact },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Audi Q7", DriveType = Domain.DriveType.AWD, SeatCount = 7, BodyType = BodyType.SUV, VehicleClass = VehicleClass.SUV },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Ford Focus", DriveType = Domain.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Hatchback, VehicleClass = VehicleClass.Compact },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Volkswagen Golf", DriveType = Domain.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Hatchback, VehicleClass = VehicleClass.Compact },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Toyota RAV4", DriveType = Domain.DriveType.AWD, SeatCount = 5, BodyType = BodyType.SUV, VehicleClass = VehicleClass.SUV },
                new VehicleModel { Id = Guid.NewGuid(), Name = "Chevrolet Cobalt", DriveType = Domain.DriveType.FWD, SeatCount = 5, BodyType = BodyType.Sedan, VehicleClass = VehicleClass.Compact },
                new VehicleModel { Id = Guid.NewGuid(), Name = "BMW X5", DriveType = Domain.DriveType.AWD, SeatCount = 5, BodyType = BodyType.SUV, VehicleClass = VehicleClass.SUV },
            });
    }

    /// <summary>
    /// Generates test vehicle generations based on models
    /// </summary>
    private void GenerateGenerations()
    {
        var random = new Random();

        foreach (var model in Models)
        {
            var baseYear = 2018 + Models.IndexOf(model);
            var generationCount = Models.IndexOf(model) % 2 + 1;

            for (var i = 0; i < generationCount; i++)
            {
                Generations.Add(new VehicleGeneration
                {
                    Id = Guid.NewGuid(),
                    Year = baseYear + i,
                    EngineVolume = 1.8 + (i * 0.5) + (random.NextDouble() * 1.0),
                    Transmission = GetTransmissionForModel(model.Name, i),
                    PricePerHour = CalculatePricePerHour(model.VehicleClass, i),
                    Model = model
                });
            }
        }
    }

    /// <summary>
    /// Generates test vehicles based on generations
    /// </summary>
    private void GenerateVehicles()
    {
        var colors = new[] { "Black", "White", "Silver", "Gray", "Red", "Blue", "Yellow" };
        var random = new Random();

        foreach (var generation in Generations)
        {
            var vehiclesPerGeneration = 2;

            for (var i = 0; i < vehiclesPerGeneration; i++)
            {
                Vehicles.Add(new Vehicle
                {
                    Id = Guid.NewGuid(),
                    LicensePlate = GenerateLicensePlate(),
                    Color = colors[random.Next(colors.Length)],
                    Generation = generation
                });
            }
        }
    }

    /// <summary>
    /// Generates test renters
    /// </summary>
    private void GenerateRenters()
    {
        var rentersData = new[]
        {
                new { Name = "Иван Иванов", License = "А123БВ", BirthYear = 2003 },
                new { Name = "Мария Петрова", License = "В456ГД", BirthYear = 1990 },
                new { Name = "Алексей Баранкин", License = "Е789ЖЗ", BirthYear = 1992 },
                new { Name = "Екатерина Новикова", License = "К012ЛМ", BirthYear = 1991 },
                new { Name = "Дмитрий Никитин", License = "Н345ПР", BirthYear = 1975 },
                new { Name = "Юлия Белова", License = "С678ТУ", BirthYear = 2000 },
                new { Name = "Анна Скрипка", License = "Ч234ШЩ", BirthYear = 1991 },
                new { Name = "Наталья Лебедева", License = "Я890АБ", BirthYear = 1989 },
                new { Name = "Андрей Соколов", License = "У123ФХ", BirthYear = 1993 },
                new { Name = "Ирина Воробьева", License = "Ц456ЧШ", BirthYear = 1997 }
            };

        foreach (var renterData in rentersData)
        {
            Renters.Add(new Renter
            {
                Id = Guid.NewGuid(),
                LicenseNumber = renterData.License,
                FullName = renterData.Name,
                DateOfBirth = new DateTime(renterData.BirthYear, 1, 1)
            });
        }
    }

    /// <summary>
    /// Generates test rental transactions
    /// </summary>
    private void GenerateRentals()
    {
        var random = new Random();
        var startDate = new DateTime(2024, 1, 1);

        for (var i = 0; i < 15; i++)
        {
            var vehicle = Vehicles[random.Next(Vehicles.Count)];
            var renter = Renters[random.Next(Renters.Count)];
            var durationHours = random.Next(1, 72);

            Rentals.Add(new Rental
            {
                Id = Guid.NewGuid(),
                RentStartTime = startDate.AddDays(i * 3).AddHours(random.Next(24)),
                DurationHours = durationHours,
                Car = vehicle,
                Renter = renter
            });
        }
    }

    /// <summary>
    /// Determines transmission type for a vehicle model
    /// </summary>
    /// <param name="modelName">Model name</param>
    /// <param name="generationIndex">Generation index</param>
    /// <returns>Transmission type</returns>
    private static Transmission GetTransmissionForModel(string modelName, int generationIndex)
    {
        if (modelName.Contains("Porsche") || modelName.Contains("BMW") || generationIndex == 0)
            return Transmission.Automatic;

        return generationIndex % 2 == 0 ? Transmission.Automatic : Transmission.Manual;
    }

    /// <summary>
    /// Calculates rental price per hour for vehicle class and generation
    /// </summary>
    /// <param name="vehicleClass">Vehicle class</param>
    /// <param name="generationIndex">Generation index</param>
    /// <returns>Rental price per hour in rubles</returns>
    private static decimal CalculatePricePerHour(VehicleClass vehicleClass, int generationIndex)
    {
        var basePrice = vehicleClass switch
        {
            VehicleClass.Mini => 800,
            VehicleClass.Economy => 1000,
            VehicleClass.Compact => 1200,
            VehicleClass.Family => 1500,
            VehicleClass.Business => 2500,
            VehicleClass.Luxury => 4000,
            VehicleClass.SUV => 2000,
            VehicleClass.Minivan => 1800,
            VehicleClass.Convertible => 3000,
            VehicleClass.Sports => 5000,
            _ => 1000
        };

        var generationMultiplier = 1.0m + (generationIndex * 0.05m);
        return basePrice * generationMultiplier;
    }

    /// <summary>
    /// Generates a random Russian license plate
    /// </summary>
    /// <returns>License plate in format X123XX</returns>
    private static string GenerateLicensePlate()
    {
        var random = new Random();
        var letters = "АВЕКМНОРСТУХ";

        var letter1 = letters[random.Next(letters.Length)];
        var digits = random.Next(100, 1000).ToString("D3");
        var letter2 = letters[random.Next(letters.Length)];
        var letter3 = letters[random.Next(letters.Length)];

        return $"{letter1}{digits}{letter2}{letter3}";
    }
}