using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models.Entities
{
    public class Inspection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime InspectionDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Result { get; set; }

        public int TotalScore { get; set; }

        public InspectionStatus Status { get; set; } = InspectionStatus.Scheduled;
        // Foreign Key
        [Required]
        public Guid AppointmentId { get; set; }

        // Navigation property
        public virtual Appointment Appointment { get; set; }
        public virtual ICollection<Checkpoint> Checkpoints { get; set; } = new HashSet<Checkpoint>();
    }
}
