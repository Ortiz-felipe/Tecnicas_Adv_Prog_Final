namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionOperationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public InspectionDetailsDto InspectionDetails { get; set; }
    }
}
