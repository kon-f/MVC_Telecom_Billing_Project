using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class Bill
    {
        [Key]
        public int Bill_ID { get; set; } // Primary Key

        [Required]
        [ForeignKey("Phone")]
        public string PhoneNumber { get; set; } // Foreign Key from Phone
        public Phone Phone { get; set; } // Navigation Property

        [Required]
        public decimal Costs { get; set; } // Total Costs

        [Required]
        public bool Paid { get; set; } // Indicates if the bill is paid
    }
}
