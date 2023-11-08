using VTVApp.Api.Models.DTOs.Inspections;

namespace VTVApp.Api.Models.DTOs.Checkpoints
{
    public class CheckpointDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Detailed stats or history about the checkpoint performance.
        public double AverageScore { get; set; }
        public int TimesChecked { get; set; }
        public List<InspectionCheckpointGradeDto> Grades { get; set; }
    }
}
