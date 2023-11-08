namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionCheckpointGradeDto
    {
        public Guid CheckpointId { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
        // Include properties related to the inspection or the inspector.
        public Guid InspectionId { get; set; }
        public Guid InspectorId { get; set; }
    }
}
