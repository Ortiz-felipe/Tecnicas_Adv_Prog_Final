using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models
{
    public class Check
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public int ScoreObtained { get; set; }

        [Required]
        public Guid AppointmentID { get; set; }

        [Required]
        public int CheckpointID { get; set; }

        public Appointment Appointment { get; set; }
        public Checkpoint Checkpoint { get; set; }
    }
}
