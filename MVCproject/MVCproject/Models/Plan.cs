using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    [Table("Programs")] // Matches the database table name since the class is named differently
    public class Plan
    {
        [Key]
        [MaxLength(50)]
        public string ProgramName { get; set; } // Primary Key

        [Required]
        public string Benefits { get; set; } // Benefits (TEXT)

        [Required]
        public decimal Charge { get; set; } // Charge
    }
}
