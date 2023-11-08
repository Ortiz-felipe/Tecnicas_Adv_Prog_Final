namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class CheckpointResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Include a confirmation message or additional relevant data.
        public string Message { get; set; }
    }
}
