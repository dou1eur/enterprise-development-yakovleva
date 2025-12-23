namespace CarRentalService.TestData.Constants;

/// <summary>
/// Constants for test data used across all layers
/// </summary>
public static class VehicleIds
{
    public static readonly Guid CobaltVehicle1 = Guid.Parse("11111111-7777-7777-7777-777777777777");
    public static readonly Guid CobaltVehicle2 = Guid.Parse("22222222-8888-8888-8888-888888888888");
    public static readonly Guid CamryVehicle = Guid.Parse("33333333-9999-9999-9999-999999999999");
    public static readonly Guid BmwX5Vehicle = Guid.Parse("44444444-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
    public static readonly Guid FordTransitVehicle = Guid.Parse("55555555-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
    public static readonly Guid TeslaVehicle = Guid.Parse("66666666-CCCC-CCCC-CCCC-CCCCCCCCCCCC");

    public static readonly Guid[] All =
    [
        CobaltVehicle1,
        CobaltVehicle2,
        CamryVehicle,
        BmwX5Vehicle,
        FordTransitVehicle,
        TeslaVehicle
    ];
}

public static class RenterIds
{
    public static readonly Guid Renter1 = Guid.Parse("77777777-1111-1111-1111-111111111111");
    public static readonly Guid Renter2 = Guid.Parse("88888888-2222-2222-2222-222222222222");
    public static readonly Guid Renter3 = Guid.Parse("99999999-3333-3333-3333-333333333333");
    public static readonly Guid Renter4 = Guid.Parse("AAAAAAAA-4444-4444-4444-444444444444");
    public static readonly Guid Renter5 = Guid.Parse("BBBBBBBB-5555-5555-5555-555555555555");

    public static readonly Guid[] All =
    [
        Renter1,
        Renter2,
        Renter3,
        Renter4,
        Renter5
    ];
}

public static class VehicleModelIds
{
    public static readonly Guid ChevroletCobaltModel = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid ToyotaCamryModel = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid BmwX5Model = Guid.Parse("33333333-3333-3333-3333-333333333333");
    public static readonly Guid FordTransitModel = Guid.Parse("44444444-4444-4444-4444-444444444444");
    public static readonly Guid TeslaModel3 = Guid.Parse("55555555-5555-5555-5555-555555555555");

    public static readonly Guid[] All =
    [
        ChevroletCobaltModel,
        ToyotaCamryModel,
        BmwX5Model,
        FordTransitModel,
        TeslaModel3
    ];
}

public static class ModelGenerationIds
{
    public static readonly Guid CobaltGeneration = Guid.Parse("11111111-2222-2222-2222-222222222222");
    public static readonly Guid CamryGeneration = Guid.Parse("22222222-3333-3333-3333-333333333333");
    public static readonly Guid BmwX5Generation = Guid.Parse("33333333-4444-4444-4444-444444444444");
    public static readonly Guid FordTransitGeneration = Guid.Parse("44444444-5555-5555-5555-555555555555");
    public static readonly Guid TeslaGeneration = Guid.Parse("55555555-6666-6666-6666-666666666666");

    public static readonly Guid[] All =
    [
        CobaltGeneration,
        CamryGeneration,
        BmwX5Generation,
        FordTransitGeneration,
        TeslaGeneration
    ];
}

public static class RentalIds
{
    public static readonly Guid Rental1 = Guid.Parse("CCCCCCCC-1111-1111-1111-111111111111");
    public static readonly Guid Rental2 = Guid.Parse("DDDDDDDD-2222-2222-2222-222222222222");
    public static readonly Guid Rental3 = Guid.Parse("EEEEEEEE-3333-3333-3333-333333333333");
    public static readonly Guid Rental4 = Guid.Parse("FFFFFFFF-4444-4444-4444-444444444444");
    public static readonly Guid Rental5 = Guid.Parse("11111111-5555-5555-5555-555555555555");
    public static readonly Guid Rental6 = Guid.Parse("22222222-6666-6666-6666-666666666666");
    public static readonly Guid Rental7 = Guid.Parse("33333333-7777-7777-7777-777777777777");
    public static readonly Guid Rental8 = Guid.Parse("44444444-8888-8888-8888-888888888888");
    public static readonly Guid Rental9 = Guid.Parse("55555555-9999-9999-9999-999999999999");
    public static readonly Guid CurrentRental1 = Guid.Parse("66666666-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
    public static readonly Guid CurrentRental2 = Guid.Parse("77777777-BBBB-BBBB-BBBB-BBBBBBBBBBBB");
}
public static class TestDataConstants
{
    /// <summary>
    /// Gets all vehicle IDs
    /// </summary>
    public static Guid[] Vehicles => VehicleIds.All;

    /// <summary>
    /// Gets all renter IDs
    /// </summary>
    public static Guid[] Renters => RenterIds.All;

    /// <summary>
    /// Gets all vehicle model IDs
    /// </summary>
    public static Guid[] VehicleModels => VehicleModelIds.All;

    /// <summary>
    /// Gets all model generation IDs
    /// </summary>
    public static Guid[] ModelGenerations => ModelGenerationIds.All;

    /// <summary>
    /// Validates that test data constants are properly configured
    /// </summary>
    public static void Validate()
    {
        if (Vehicles.Length == 0)
            throw new InvalidOperationException("No vehicle IDs defined in TestDataConstants");

        if (Renters.Length == 0)
            throw new InvalidOperationException("No renter IDs defined in TestDataConstants");

        if (VehicleModels.Length == 0)
            throw new InvalidOperationException("No vehicle model IDs defined in TestDataConstants");

        if (ModelGenerations.Length == 0)
            throw new InvalidOperationException("No model generation IDs defined in TestDataConstants");

        Console.WriteLine($"TestDataConstants validated: {Vehicles.Length} vehicles, {Renters.Length} renters");
    }
}