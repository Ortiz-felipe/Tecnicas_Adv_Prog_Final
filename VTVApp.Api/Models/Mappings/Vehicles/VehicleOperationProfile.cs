using AutoMapper;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Vehicles
{
    public class VehicleOperationProfile : Profile
    {
        public VehicleOperationProfile()
        {
            CreateMap<Vehicle, VehicleOperationResultDto>()
                .ForMember(dest => dest.Id, src => src.MapFrom(opt => opt.Id))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true)) // Assuming success if this mapping is used
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Vehicle created successfully."));
        }
    }
}
