namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class ScheduleInspectionRecheckDto
    {
        public Guid InspectionId { get; set; }
        public DateTime RecheckDate { get; set; }
        public string Comments { get; set; } // Additional details about the scheduling
    }
}
