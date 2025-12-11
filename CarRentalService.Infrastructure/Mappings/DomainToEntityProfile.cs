using AutoMapper;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Entities;

namespace CarRentalService.Infrastructure.Mappings;

public class DomainToEntityProfile : Profile
{
    public DomainToEntityProfile()
    {
        CreateMap<Renter, RenterEntity>();
        CreateMap<Vehicle, VehicleEntity>()
            .ForMember(dest => dest.ModelGenerationId, opt => opt.MapFrom(src => src.GenerationId));
        CreateMap<VehicleModel, VehicleModelEntity>();
        CreateMap<ModelGeneration, ModelGenerationEntity>();
        CreateMap<Rental, RentalEntity>();
    }
}