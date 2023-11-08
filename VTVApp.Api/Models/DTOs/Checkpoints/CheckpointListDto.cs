namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class CheckpointListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        // Include information about average scores or number of times a checkpoint has failed, if relevant.
        public int Score { get; set; }
        public int FailureCount { get; set; }
    }
}
