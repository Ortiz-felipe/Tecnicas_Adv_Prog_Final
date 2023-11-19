using AutoMapper;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Inspections
{
    public class InspectionListProfile : Profile
    {
        public InspectionListProfile()
        {
            CreateMap<Inspection, InspectionListDto>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.Appointment.VehicleId))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.Appointment.Vehicle.LicensePlate))
                .ForMember(dest => dest.InspectionDate, opt => opt.MapFrom(src => src.InspectionDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }

        private string DetermineInspectionOutcome(Appointment appointment)
        {
            var now = DateTime.Now;
            var inspection = appointment.Inspection;

            if (inspection == null || appointment.Date > now)
            {
                return "In Review"; // Replace with your actual status
            }

            if (inspection.InspectionDate.AddYears(1) < now)
            {
                return "Expired"; // Replace with your actual status
            }

            if (inspection.Checkpoints.Any(c => c.Score <= 5))
            {
                return "Failed"; // Replace with your actual status
            }

            return inspection.TotalScore switch
            {
                < 40 => "Failed", // Replace with your actual status
                >= 80 => "Approved", // Replace with your actual status
                _ => "In Review" // Replace with your actual status or other statuses as per your logic
            };
        }
    }
}
