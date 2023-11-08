namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class CheckpointDeletedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool WasDeleted { get; set; }
        public string Message { get; set; }
    }
}
