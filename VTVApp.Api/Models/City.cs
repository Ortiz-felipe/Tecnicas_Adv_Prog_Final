using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        public int ProvinceId { get; set; }

        public Province Province { get; set; }
    }
}
