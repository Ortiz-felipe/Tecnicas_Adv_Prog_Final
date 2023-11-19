using AutoMapper;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Models.Mappings.Vehicles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.isFavorite, opt => opt.MapFrom(src => src.IsFavorite))
                .ForMember(dest => dest.VehicleStatus, opt => opt.MapFrom(src => DetermineVehicleStatus(src)));

        }

        private int DetermineVehicleStatus(Vehicle vehicle)
        {
            var now = DateTime.Now;
            var latestInspection = vehicle.Appointments
                .Where(a => a.Inspection != null)
                .OrderByDescending(a => a.Inspection.InspectionDate)
                .Select(a => a.Inspection)
                .FirstOrDefault();

            // If there is no inspection at all, we might assume it's scheduled or pending.
            if (latestInspection == null)
            {
                var hasFutureAppointment = vehicle.Appointments.Any(a => a.Date >= now);
                return hasFutureAppointment ? (int)InspectionStatus.Scheduled : 9; // Or another default status
            }

            // If the latest inspection is older than one year, it's expired
            if (latestInspection.InspectionDate.AddYears(1) < now)
            {
                return (int)InspectionStatus.Expired;
            }

            // If any checkpoint score is less or equal than 5, the inspection has failed.
            if (latestInspection.Checkpoints.Any(c => c.Score <= 5))
            {
                return (int)InspectionStatus.CompletedFailed;
            }

            return latestInspection.TotalScore switch
            {
                // If the total score is 40 or less, the inspection has failed.
                <= 40 => (int)InspectionStatus.CompletedFailed,
                // If the total score is 80 or more, the inspection is approved.
                >= 80 => (int)InspectionStatus.CompletedApproved,
                // If the code reaches this point, you may want to handle inspections that are between scores of 41 and 79.
                // Depending on your business logic, this could be 'InReview', 'Pending', 'Observation', etc.
                _ => (int)InspectionStatus.InReview
            };

            
        }
    }
}
