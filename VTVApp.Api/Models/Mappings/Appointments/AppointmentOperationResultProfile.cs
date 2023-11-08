using System.Globalization;
using AutoMapper;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Appointments
{
    public class AppointmentOperationResultProfile : Profile
    {
        public AppointmentOperationResultProfile()
        {
            CreateMap<Appointment, AppointmentOperationResultDto>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ScheduledDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true)) // Assuming success if this mapping is used
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Appointment scheduled successfully."));
        }
    }
}
