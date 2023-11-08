namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionListDto
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public string VehicleIdentificationNumber { get; set; }  // If VIN is needed for a quick reference
        public DateTime InspectionDate { get; set; }
        public string Status { get; set; } // Open, Completed, Failed, etc.
    }
}
