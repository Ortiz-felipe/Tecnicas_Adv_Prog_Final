using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models.Entities
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        // Foreign Keys
        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        // Navigation properties
        public virtual Vehicle Vehicle { get; set; }
        public virtual User User { get; set; }
        public virtual Inspection Inspection { get; set; }
    }
}