using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models
{
    public class Checkpoint
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Check> Checks { get; set; } = new List<Check>();
    }
}
