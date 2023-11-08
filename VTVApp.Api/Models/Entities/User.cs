using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public UserRole Role { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        // Navigation properties
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public virtual City City { get; set; }
        public virtual Province Province { get; set; }
    }
}
