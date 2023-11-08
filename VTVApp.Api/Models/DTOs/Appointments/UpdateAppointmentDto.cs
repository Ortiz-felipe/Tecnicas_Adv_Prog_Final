namespace VTVApp.Api.Models.DTOs.Appointments
{
    public class UpdateAppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime NewAppointmentDate { get; set; }
        // Consider whether other aspects of the appointment can be updated, such as vehicle details.
    }
}
