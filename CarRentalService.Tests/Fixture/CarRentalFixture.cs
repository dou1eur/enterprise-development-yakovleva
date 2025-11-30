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
    public List<VehicleModel> Models { get; } = new();

    /// <summary>
    /// Collection of vehicle generations
    /// </summary>
    public List<ModelGeneration> Generations { get; } = new();

    /// <summary>
    /// Collection of specific vehicles
    /// </summary>
    public List<Vehicle> Vehicles { get; } = new();

    /// <summary>
    /// Collection of renters
    /// </summary>
    public List<Renter> Renters { get; } = new();

    /// <summary>
    /// Collection of rental transactions
    /// </summary>
    public List<Rental> Rentals { get; } = new();

    private VehicleModel? ChevroletCobaltModel { get; set; }
    private VehicleModel? ToyotaCamryModel { get; set; }
    private ModelGeneration? CobaltGeneration { get; set; }
    private ModelGeneration? CamryGeneration { get; set; }
    private Vehicle? CobaltVehicle1 { get; set; }
    private Vehicle? CobaltVehicle2 { get; set; }
    private Vehicle? CamryVehicle { get; set; }
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
            Id = Guid.NewGuid(),
            Name = "Chevrolet Cobalt",
            DriveType = Domain.DriveType.Fwd,
            SeatCount = 5,
            BodyType = BodyType.Sedan,
            VehicleClass = VehicleClass.Compact
        };

        ToyotaCamryModel = new VehicleModel
        {
            Id = Guid.NewGuid(),
            Name = "Toyota Camry",
            DriveType = Domain.DriveType.Fwd,
            SeatCount = 5,
            BodyType = BodyType.Sedan,
            VehicleClass = VehicleClass.Family
        };

        Models.Add(ChevroletCobaltModel);
        Models.Add(ToyotaCamryModel);
    }

    /// <summary>
    /// Generates test vehicle generations based on models
    /// </summary>
    private void CreateGenerations()
    {
        CobaltGeneration = new ModelGeneration
        {
            Id = Guid.NewGuid(),
            Year = 2016,
            EngineVolume = 1.8,
            Transmission = Transmission.Automatic,
            RentalPricePerHour = 1200m,
            VehicleModelId = ChevroletCobaltModel!.Id
        };

        CamryGeneration = new ModelGeneration
        {
            Id = Guid.NewGuid(),
            Year = 2023,
            EngineVolume = 2.0,
            Transmission = Transmission.Automatic,
            RentalPricePerHour = 1500m,
            VehicleModelId = ToyotaCamryModel!.Id
        };

        Generations.Add(CobaltGeneration);
        Generations.Add(CamryGeneration);
    }

    /// <summary>
    /// Generates test vehicles based on generations
    /// </summary>
    private void CreateVehicles()
    {
        CobaltVehicle1 = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = "Н099ОР",
            Color = "Black",
            GenerationId = CobaltGeneration!.Id
        };

        CobaltVehicle2 = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = "А071ВР",
            Color = "White",
            GenerationId = CobaltGeneration.Id
        };

        CamryVehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            LicensePlate = "Т801УХ",
            Color = "Silver",
            GenerationId = CamryGeneration!.Id
        };

        Vehicles.Add(CobaltVehicle1);
        Vehicles.Add(CobaltVehicle2);
        Vehicles.Add(CamryVehicle);
    }

    /// <summary>
    /// Generates test renters
    /// </summary>
    private void CreateRenters()
    {
        Renter1 = new Renter
        {
            Id = Guid.NewGuid(),
            LicenseNumber = "1234123412",
            FullName = "Андрей Петров",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        Renter2 = new Renter
        {
            Id = Guid.NewGuid(),
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
            Id = Guid.NewGuid(),
            RentStartTime = new DateTime(2024, 1, 1, 10, 0, 0),
            DurationHours = 5,
            VehicleId = CobaltVehicle1!.Id,
            RenterId = Renter1!.Id,
            TotalCost = 1200m * 5
        });

        Rentals.Add(new Rental
        {
            Id = Guid.NewGuid(),
            RentStartTime = new DateTime(2024, 1, 2, 14, 0, 0),
            DurationHours = 3,
            VehicleId = CobaltVehicle1!.Id,
            RenterId = Renter2!.Id,
            TotalCost = 1200m * 3
        });

        Rentals.Add(new Rental
        {
            Id = Guid.NewGuid(),
            RentStartTime = new DateTime(2024, 1, 3, 9, 0, 0),
            DurationHours = 8,
            VehicleId = CobaltVehicle2!.Id,
            RenterId = Renter1!.Id,
            TotalCost = 1200m * 8
        });

        Rentals.Add(new Rental
        {
            Id = Guid.NewGuid(),
            RentStartTime = new DateTime(2024, 1, 4, 11, 0, 0),
            DurationHours = 6,
            VehicleId = CamryVehicle!.Id,
            RenterId = Renter2!.Id,
            TotalCost = 1500m * 6
        });
    }
    public VehicleModel GetVehicleModelById(Guid id) => Models.First(m => m.Id == id);
    public ModelGeneration GetModelGenerationById(Guid id) => Generations.First(g => g.Id == id);
    public Vehicle GetVehicleById(Guid id) => Vehicles.First(v => v.Id == id);
    public Renter GetRenterById(Guid id) => Renters.First(r => r.Id == id);
}