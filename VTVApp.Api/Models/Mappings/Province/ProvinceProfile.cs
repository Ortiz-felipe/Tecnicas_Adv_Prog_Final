using AutoMapper;
using VTVApp.Api.Models.DTOs.Provinces;

namespace VTVApp.Api.Models.Mappings.Province
{
    public class ProvinceProfile : Profile
    {
        public ProvinceProfile()
        {
            CreateMap<Entities.Province, ProvinceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Map ProvinceId to Id if they are named differently
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                // Add other property mappings if necessary
                .ReverseMap(); // This allows mapping from ProvinceDto to Province as well
        }
    }
}
