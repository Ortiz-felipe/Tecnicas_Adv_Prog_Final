using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // For role-based actions, if applicable
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public ICollection<AppointmentListDto> Appointments { get; set; }
        public ICollection<VehicleDto> Vehicles { get; set; }
        // Consider including related entities like appointments, vehicles, etc., if needed
    }
}
