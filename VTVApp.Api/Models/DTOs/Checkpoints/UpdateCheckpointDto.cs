namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class UpdateCheckpointDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Properties for updating criteria or thresholds.
        public int MinimumSafeScore { get; set; }
    }
}
