using AutoMapper;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Appointments
{
    public class CreateAppointmentProfile : Profile
    {
        public CreateAppointmentProfile()
        {
            CreateMap<CreateAppointmentDto, Appointment>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.AppointmentDate.Date))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.AppointmentDate.TimeOfDay));
        }
    }
}
