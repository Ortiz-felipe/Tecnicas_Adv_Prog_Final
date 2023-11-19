using AutoMapper;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Vehicles
{
    public class CreateVehicleProfile : Profile
    {
        public CreateVehicleProfile()
        {
            CreateMap<CreateVehicleDto, Vehicle>()
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand)) // Assuming 'Name' corresponds to 'Make'
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year)) // Ignoring Year since it's not in CreateVehicleDto
                .ForMember(dest => dest.Appointments, opt => opt.Ignore()); // Ignoring Appointments as it's a new vehicle
        }
    }
}
