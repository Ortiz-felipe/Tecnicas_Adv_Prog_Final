using VTVApp.Api.Models.DTOs.Checkpoints;

namespace VTVApp.Api.Models.DTOs.Inspections
{
    public class CreateInspectionDto
    {
        public Guid VehicleId { get; set; }
        public DateTime InspectionDate { get; set; }
        public int Result { get; set; }
        public Guid AppointmentId { get; set; }

        public IEnumerable<CheckpointListDto> Checkpoints { get; set; }
    }
}
