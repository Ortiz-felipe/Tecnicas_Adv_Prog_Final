using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models
{
    public class Status
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
