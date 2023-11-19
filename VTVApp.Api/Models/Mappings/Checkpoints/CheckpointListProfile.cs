using AutoMapper;
using VTVApp.Api.Models.DTOs.Checkpoints;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Checkpoints
{
    public class CheckpointListProfile : Profile
    {
        public CheckpointListProfile()
        {
            CreateMap<CheckpointListDto, Checkpoint>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));
        }
    }
}
