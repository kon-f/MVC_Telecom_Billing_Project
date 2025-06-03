using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class Call
    {
        [Key]
        public int Call_ID { get; set; } // Primary Key

        [Required]
        public string Description { get; set; } // Call Description
    }
}
