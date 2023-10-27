using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
