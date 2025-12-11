using AutoMapper;
using CarRentalService.Domain;
using CarRentalService.Infrastructure.Entities;

namespace CarRentalService.Infrastructure.Mappings;

public class EntityToDomainProfile : Profile
{
    public EntityToDomainProfile()
    {
        CreateMap<RenterEntity, Renter>();
        CreateMap<VehicleEntity, Vehicle>()
            .ForMember(dest => dest.GenerationId, opt => opt.MapFrom(src => src.ModelGenerationId));
        CreateMap<VehicleModelEntity, VehicleModel>();
        CreateMap<ModelGenerationEntity, ModelGeneration>();
        CreateMap<RentalEntity, Rental>();
    }
}