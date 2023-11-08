namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionCheckpointDetailDto
    {
        public Guid CheckpointId { get; set; }
        public string CheckpointName { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
        // Might also include the recommended action if the checkpoint fails
        public string RecommendedAction { get; set; }
    }
}
