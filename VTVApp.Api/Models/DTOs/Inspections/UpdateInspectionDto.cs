using VTVApp.Api.Models.DTOs.Checkpoints;

namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class UpdateInspectionDto
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Result { get; set; }
        public int TotalScore { get; set; }
        public IEnumerable<CheckpointListDto> UpdatedCheckpoints { get; set; }
    }
}
