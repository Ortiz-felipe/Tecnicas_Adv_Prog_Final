using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VTVApp.Api.Models.Entities
{
    public class Checkpoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Score { get; set; }

        [StringLength(500)]
        public string Comment { get; set; } // Optional for inspector comments

        // Foreign Key
        [Required]
        public Guid InspectionId { get; set; }

        // Navigation property
        public virtual Inspection Inspection { get; set; }
    }
}
