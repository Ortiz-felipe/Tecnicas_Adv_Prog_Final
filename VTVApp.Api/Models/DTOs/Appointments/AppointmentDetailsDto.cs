using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.Api.Models.DTOs.Appointments
{
    public class AppointmentDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public UserDto User { get; set; } // Nested DTO for user information.
        public VehicleDto Vehicle { get; set; } // Nested DTO for vehicle information.
        public InspectionOutcomeDto InspectionOutcome { get; set; } // Nested DTO for inspection results, if already inspected.
        // Include any additional details that may be necessary for the inspection process.
    }
}
