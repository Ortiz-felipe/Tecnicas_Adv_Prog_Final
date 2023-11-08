namespace VTVApp.Api.Models.DTOs.Appointments
{
    public class AvailableAppointmentSlotsDto
    {
        public IEnumerable<DateTime> AvailableSlots { get; set; }
        // Could include other relevant information like the duration of each slot, etc.
    }
}
