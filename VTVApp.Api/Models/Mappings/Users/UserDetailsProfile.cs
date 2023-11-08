using AutoMapper;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Users
{
    public class UserDetailsProfile : Profile
    {
        public UserDetailsProfile()
        {
            CreateMap<User, UserDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString())) // Assuming UserRole is an enum
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name)) // Assuming City has a Name property
                .ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province.Name)) // Assuming Province has a Name property
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments))
                .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles));
        }
    }
}
