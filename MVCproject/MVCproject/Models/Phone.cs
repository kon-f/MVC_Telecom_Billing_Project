using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class Phone
    {
        [Key]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } // Primary Key

        [ForeignKey("Plan")]
        public string Program_Name { get; set; } // Foreign Key from Programs/Plans
        public Plan Plan { get; set; } // Navigation Property
    }
}
