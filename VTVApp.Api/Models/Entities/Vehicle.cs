using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(10)]
        public string LicensePlate { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Range(1900, 3000)] // Adjust the range according to your needs
        public int Year { get; set; }

        [Required]
        [StringLength(30)]
        public string Color { get; set; }

        // Foreign Key
        [Required]
        public Guid UserId { get; set; }

        // Navigation property
        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}