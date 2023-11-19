namespace VTVApp.Api.Models.Entities
{
    public enum AppointmentStatus
    {
        Pending,    // The appointment is yet to start
        Canceled,   // The appointment was canceled
        Attended,   // The user attended the appointment
        Completed   // The appointment was completed

    }
}
