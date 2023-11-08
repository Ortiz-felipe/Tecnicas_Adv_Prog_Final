namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class CreateInspectionDto
    {
        public Guid VehicleId { get; set; }
        public DateTime InspectionDate { get; set; }
        public List<Guid> CheckpointIds { get; set; } // List of checkpoint IDs to be included in the inspection
        public string InspectorName { get; set; } // Name of the inspector performing the inspection
        // Other properties as required, such as notes or initial comments
    }
}
