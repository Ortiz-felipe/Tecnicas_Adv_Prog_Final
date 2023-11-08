namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class CreateInspectionResultDto
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public DateTime ScheduledDate { get; set; }
        // Confirmation or error message
        public string Message { get; set; }
    }
}
