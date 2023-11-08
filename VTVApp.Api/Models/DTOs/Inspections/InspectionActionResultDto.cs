namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionActionResultDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } // Success, failure, or other messages
    }
}
