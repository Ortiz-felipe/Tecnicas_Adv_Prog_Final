using AutoMapper;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Inspections
{
    public class InspectionCheckpointDetailProfile : Profile
    {
        public InspectionCheckpointDetailProfile()
        {
            CreateMap<Checkpoint, InspectionCheckpointDetailDto>()
                .ForMember(dest => dest.CheckpointId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CheckpointName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));
        }
    }
}
