using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VTVApp.Api.Models
{
    public class Appointment
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required, DefaultValue(1)]
        public int StatusID { get; set; }

        [Required]
        public int TotalScore { get; set; }

        public Status Status { get; set; }
        public ICollection<Check> Checks { get; set; } = new List<Check>();
    }
}
