using AutoMapper;
using VTVApp.Api.Models.DTOs.Cities;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Cities
{
    public class CityDetailsProfile : Profile
    {
        public CityDetailsProfile()
        {
            // Mapping from City entity to CityDetailsDto
            CreateMap<City, CityDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Assuming CityId is the ID property in the City entity
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode)) // Assuming there is a navigation property to Province
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province))
                .ReverseMap(); // This allows mapping from CityDetailsDto to City, assuming you need to update or create cities from CityDetailsDto.
        }
    }
}
