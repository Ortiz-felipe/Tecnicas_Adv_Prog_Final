using AutoMapper;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Inspections
{
    public class InspectionOperationResultProfile : Profile
    {
        public InspectionOperationResultProfile()
        {
            CreateMap<Inspection, InspectionOperationResultDto>()
                .ForPath(dest => dest.InspectionDetails.Id, src => src.MapFrom(opt => opt.Id))
                .ForPath(dest => dest.InspectionDetails.InspectionDate, src => src.MapFrom(opt => opt.InspectionDate))
                .ForPath(dest => dest.InspectionDetails.VehicleId, src => src.MapFrom(opt => opt.Appointment.VehicleId))
                .ForPath(dest => dest.InspectionDetails.Checkpoints, src => src.MapFrom(opt => opt.Checkpoints))
                .ForMember(dest => dest.Success, opt => opt.MapFrom(src => true)) // Assuming success if this mapping is used
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "Inspection created successfully."));
                
        }
    }
}
