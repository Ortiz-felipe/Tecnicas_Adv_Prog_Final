namespace VTVApp.Api.Models.DTOs.Appointments
{
    public class AppointmentListDto
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string UserFullName { get; set; } // Assuming this is composed from User's first and last name.
        public string VehicleLicensePlate { get; set; }
        // Add more properties if needed, such as status of appointment (scheduled, completed, etc.)
    }
}
