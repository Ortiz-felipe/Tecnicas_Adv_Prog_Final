using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string LicencePlate { get; set; }

        [Required]
        public string DNI { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string TelephoneNumber { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        public City City { get; set; }
        public Province Province { get; set; }

    }
}
