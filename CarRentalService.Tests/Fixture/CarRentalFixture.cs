using CarRentalService.Domain;

namespace CarRentalService.Tests.Fixture;

/// <summary>
/// Provides test data for car rental service
/// </summary>
public class CarRentalFixture
{
    /// <summary>
    /// Collection of vehicle models
    /// </summary>
    public List<VehicleModel> Models { get; } = [];

    /// <summary>
    /// Collection of vehicle generations
    /// </summary>
    public List<VehicleGeneration> Generations { get; } = [];

    /// <summary>
    /// Collection of specific vehicles
    /// </summary>
    public List<Vehicle> Vehicles { get; } = [];

    /// <summary>
    /// Collection of renters
    /// </summary>
    public List<Renter> Renters { get; } = [];

    /// <summary>
    /// Collection of rental transactions
    /// </summary>
    public List<Rental> Rentals { get; } = [];

    private VehicleModel? ChevroletCobaltModel { get; set; }
    private Vehicle? CobaltVehicle1 { get; set; }
    private Vehicle? CobaltVehicle2 { get; set; }
    private Renter? Renter1 { get; set; }
    private Renter? Renter2 { get; set; }

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
    private void Initialize()
    {
        CreateModels();
        CreateGenerations();
        CreateVehicles();
        CreateRenters();
        CreateRentals();
    }

    /// <summary>
    /// Generates test vehicle models
    /// </summary>
    private void CreateModels()
    {
        ChevroletCobaltModel = new VehicleModel
        {
            Name = "Chevrolet Cobalt",
            DriveType = Domain.DriveType.Fwd,
            SeatCount = 5,
            BodyType = BodyType.Sedan,
            VehicleClass = VehicleClass.Compact
        };

        var toyotaCamryModel = new VehicleModel
        {
            Name = "Toyota Camry",
            DriveType = Domain.DriveType.Fwd,
            SeatCount = 5,
            BodyType = BodyType.Sedan,
            VehicleClass = VehicleClass.Family
        };

        Models.Add(ChevroletCobaltModel);
        Models.Add(toyotaCamryModel);
    }

    /// <summary>
    /// Generates test vehicle generations based on models
    /// </summary>
    private void CreateGenerations()
    {
        var cobaltGeneration = new VehicleGeneration
        {
            Year = 2016,
            EngineVolume = 1.8,
            Transmission = Transmission.Automatic,
            PricePerHour = 1200m,
            Model = ChevroletCobaltModel!
        };

        var camryGeneration = new VehicleGeneration
        {
            Year = 2023,
            EngineVolume = 2.0,
            Transmission = Transmission.Automatic,
            PricePerHour = 1500m,
            Model = Models[1]
        };

        Generations.Add(cobaltGeneration);
        Generations.Add(camryGeneration);
    }

    /// <summary>
    /// Generates test vehicles based on generations
    /// </summary>
    private void CreateVehicles()
    {
        var cobaltGeneration = Generations[0];
        var camryGeneration = Generations[1];

        CobaltVehicle1 = new Vehicle
        {
            LicensePlate = "Н099ОР",
            Color = "Black",
            Generation = cobaltGeneration
        };

        CobaltVehicle2 = new Vehicle
        {
            LicensePlate = "А071ВР",
            Color = "White",
            Generation = cobaltGeneration
        };

        var camryVehicle = new Vehicle
        {
            LicensePlate = "Т801УХ",
            Color = "Silver",
            Generation = camryGeneration
        };

        Vehicles.Add(CobaltVehicle1);
        Vehicles.Add(CobaltVehicle2);
        Vehicles.Add(camryVehicle);
    }

    /// <summary>
    /// Generates test renters
    /// </summary>
    private void CreateRenters()
    {
        Renter1 = new Renter
        {
            LicenseNumber = "1234123412",
            FullName = "Андрей Петров",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        Renter2 = new Renter
        {
            LicenseNumber = "3456345634",
            FullName = "Екатерина Новикова",
            DateOfBirth = new DateTime(1985, 1, 1)
        };

        Renters.Add(Renter1);
        Renters.Add(Renter2);
    }

    /// <summary>
    /// Generates test rental transactions
    /// </summary>
    private void CreateRentals()
    {
        Rentals.Add(new Rental
        {
            RentStartTime = new DateTime(2024, 1, 1, 10, 0, 0),
            DurationHours = 5,
            Car = CobaltVehicle1!,
            Renter = Renter1!
        });

        Rentals.Add(new Rental
        {
            RentStartTime = new DateTime(2024, 1, 2, 14, 0, 0),
            DurationHours = 3,
            Car = CobaltVehicle1!,
            Renter = Renter2!
        });

        Rentals.Add(new Rental
        {
            RentStartTime = new DateTime(2024, 1, 3, 9, 0, 0),
            DurationHours = 8,
            Car = CobaltVehicle2!,
            Renter = Renter1!
        });

        Rentals.Add(new Rental
        {
            RentStartTime = new DateTime(2024, 1, 4, 11, 0, 0),
            DurationHours = 6,
            Car = Vehicles[2],
            Renter = Renter2!
        });
    }
}