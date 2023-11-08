namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class CheckpointSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Additional summary information like average scores or failure counts.
        public double AverageScore { get; set; }
        public int FailureCount { get; set; }
    }
}
