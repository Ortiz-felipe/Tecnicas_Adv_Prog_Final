namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class InspectionRecheckRequestDto
    {
        public Guid InspectionId { get; set; }
        public List<Guid> CheckpointsToRecheck { get; set; } // IDs of checkpoints that need rechecking
        public string RequestingUserComments { get; set; } // Additional context for the recheck request
    }
}
