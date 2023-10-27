using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models
{
    public class Vehicle
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string LicensePlateNumber { get; set; }

        [Required]
        public bool VehicleStatus { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
