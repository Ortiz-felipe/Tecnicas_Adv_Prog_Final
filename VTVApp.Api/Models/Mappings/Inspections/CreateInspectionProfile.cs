using AutoMapper;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Inspections
{
    public class CreateInspectionProfile : Profile
    {
        public CreateInspectionProfile()
        {
            CreateMap<CreateInspectionDto, Inspection>()
                .ForMember(dest => dest.InspectionDate, opt => opt.MapFrom(src => src.InspectionDate))
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.AppointmentId))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
                .ForMember(dest => dest.Checkpoints, opt => opt.MapFrom(src => src.Checkpoints));

        }
    }
}
