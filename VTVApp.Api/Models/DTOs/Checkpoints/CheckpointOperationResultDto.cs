namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class CheckpointOperationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public CheckpointDetailsDto Checkpoint { get; set; }
    }
}
