using AutoMapper;
using CarRentalService.Application.Contracts.Common;
using CarRentalService.Application.Contracts.ModelGeneration;
using CarRentalService.Application.Contracts.Rental;
using CarRentalService.Application.Contracts.Renter;
using CarRentalService.Application.Contracts.Vehicle;
using CarRentalService.Application.Contracts.VehicleModel;
using CarRentalService.Domain;

namespace CarRentalService.Application.Mappings;

/// <summary>
/// AutoMapper profile for Car Rental Service
/// Configures mappings between domain entities and DTOs
/// </summary>
public class CarRentalMappingProfile : Profile
{
    public CarRentalMappingProfile()
    {
        CreateMap<ModelGeneration, ModelGenerationResponse>()
            .ConstructUsing(src => new ModelGenerationResponse(
                src.Id,
                src.Year,
                src.EngineVolume,
                src.Transmission,
                src.RentalPricePerHour,
                src.VehicleModelId
            ));

        CreateMap<Rental, RentalResponse>()
            .ConstructUsing(src => new RentalResponse(
                src.Id,
                src.RentStartTime,
                src.DurationHours,
                src.TotalCost,
                src.VehicleId,
                src.RenterId
            ));

        CreateMap<Renter, RenterResponse>()
            .ConstructUsing(src => new RenterResponse(
                src.Id,
                src.LicenseNumber,
                src.FullName,
                src.DateOfBirth
            ));

        CreateMap<Vehicle, VehicleResponse>()
            .ConstructUsing(src => new VehicleResponse(
                src.Id,
                src.LicensePlate,
                src.Color,
                src.GenerationId
            ));

        CreateMap<VehicleModel, VehicleModelResponse>()
            .ConstructUsing(src => new VehicleModelResponse(
                src.Id,
                src.Name,
                src.DriveType,
                src.SeatCount,
                src.BodyType,
                src.VehicleClass
            ));

        CreateMap<ModelGenerationRequest, ModelGeneration>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.VehicleModel, opt => opt.Ignore());

        CreateMap<RentalRequest, Rental>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalCost, opt => opt.Ignore())
            .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
            .ForMember(dest => dest.Renter, opt => opt.Ignore())
            .ForMember(dest => dest.DurationHours,
                opt => opt.MapFrom(src => src.RentalDurationHours));

        CreateMap<RenterRequest, Renter>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<VehicleRequest, Vehicle>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ModelGeneration, opt => opt.Ignore())
            .ForMember(dest => dest.GenerationId,
                opt => opt.MapFrom(src => src.ModelGenerationId));

        CreateMap<VehicleModelRequest, VehicleModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<(Vehicle Vehicle, int RentalCount), VehicleRentalCountResponse>()
            .ConstructUsing(src => new VehicleRentalCountResponse(
                MapVehicleResponse(src.Vehicle),
                src.RentalCount
            ));

        CreateMap<(Renter Renter, decimal TotalSpent), RenterTotalSpentResponse>()
            .ConstructUsing(src => new RenterTotalSpentResponse(
                MapRenterResponse(src.Renter),
                src.TotalSpent
            ));
    }

    /// <summary>
    /// Maps a Vehicle domain entity to a VehicleResponse DTO
    /// </summary>
    /// <param name="vehicle">The vehicle domain entity</param>
    /// <returns>A VehicleResponse DTO</returns>
    private static VehicleResponse MapVehicleResponse(Vehicle vehicle) =>
        new(vehicle.Id, vehicle.LicensePlate, vehicle.Color, vehicle.GenerationId);

    /// <summary>
    /// Maps a Renter domain entity to a RenterResponse DTO
    /// </summary>
    /// <param name="renter">The renter domain entity</param>
    /// <returns>A RenterResponse DTO</returns>
    private static RenterResponse MapRenterResponse(Renter renter) =>
        new(renter.Id, renter.LicenseNumber, renter.FullName, renter.DateOfBirth);
}