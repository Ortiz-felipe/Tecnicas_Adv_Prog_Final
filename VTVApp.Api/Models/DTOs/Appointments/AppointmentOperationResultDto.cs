namespace VTVApp.Api.Models.DTOs.Appointments
{
    public class AppointmentOperationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid? AppointmentId { get; set; } // Include if operation affects a specific appointment
        public DateTime ScheduledDate { get; set; }
        public IEnumerable<string> Errors { get; set; } // To provide more detailed error information if needed

    }
}
