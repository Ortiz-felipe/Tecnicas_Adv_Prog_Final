namespace VTVApp.Api.Models.DTOs.Appointments
{
    public class CreateAppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public Guid UserId { get; set; }
        public Guid VehicleId { get; set; }
        // Potentially include contact information or any other data necessary for booking an appointment.
    }
}
