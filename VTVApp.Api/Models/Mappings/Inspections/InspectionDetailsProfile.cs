using AutoMapper;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Inspections
{
    public class InspectionDetailsProfile : Profile
    {
        public InspectionDetailsProfile()
        {
            CreateMap<Inspection, InspectionDetailsDto>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.Appointment.VehicleId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InspectionDate, opt => opt.MapFrom(src => src.InspectionDate))
                .ForMember(dest => dest.Checkpoints, opt => opt.MapFrom(src => src.Checkpoints))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
