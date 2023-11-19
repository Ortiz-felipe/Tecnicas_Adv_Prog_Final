using AutoMapper;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Appointments
{
    public class AppointmentDetailsProfile : Profile
    {
        public AppointmentDetailsProfile()
        {
            CreateMap<Appointment, AppointmentDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.InspectionOutcome, opt => opt.MapFrom(src => DetermineInspectionOutcome(src)))
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle));
        }

        private InspectionStatus DetermineInspectionOutcome(Appointment appointment)
        {
            var now = DateTime.Now;
            var inspection = appointment.Inspection;

            // If this appointment has no inspection or the inspection is in the future, it's still in review or pending
            if (inspection == null || appointment.Date > now)
            {
                return InspectionStatus.InReview; // Or Scheduled/Pending based on your business rules
            }


            // If the latest inspection is older than one year, it's expired
            if (inspection.InspectionDate.AddYears(1) < now)
            {
                return InspectionStatus.Expired;
            }

            // If any checkpoint score is less or equal to 5, the inspection has failed.
            if (inspection.Checkpoints.Any(c => c.Score <= 5))
            {
                return InspectionStatus.CompletedFailed;
            }

            // Determine the outcome based on the total score
            return inspection.TotalScore switch
            {
                // If the total score is less than 40, the inspection has failed.
                < 40 => InspectionStatus.CompletedFailed,
                // If the total score is 80 or more, the inspection is approved.
                >= 80 => InspectionStatus.CompletedApproved,
                // If the code reaches this point, you may want to handle inspections that are between scores of 41 and 79.
                // Depending on your business logic, this could be 'InReview', 'Pending', 'Observation', etc.
                _ => InspectionStatus.InReview
            };
        }
    }
}
