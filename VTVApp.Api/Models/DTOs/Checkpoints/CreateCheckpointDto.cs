namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class CreateCheckpointDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        // Include properties for setting up initial criteria or thresholds.
        public int MinimumSafeScore { get; set; }
    }
}
