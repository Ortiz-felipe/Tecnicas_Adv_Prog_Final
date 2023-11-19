using AutoMapper;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Appointments
{
    public class AppointmentListProfile : Profile
    {
        public AppointmentListProfile()
        {
            CreateMap<Appointment, AppointmentListDto>()
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.VehicleLicensePlate, opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.AppointmentStatus, opt => opt.MapFrom(src => (int)src.Status));
        }
    }
}
