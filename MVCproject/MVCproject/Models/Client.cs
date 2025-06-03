using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class Client
    {
        [Key]
        public int Client_ID { get; set; } // Primary Key

        [Required]
        [MaxLength(50)]
        public string AFM { get; set; } // Tax Identification Number

        [Required]
        [ForeignKey("Phone")]
        public string PhoneNumber { get; set; } // Foreign Key from Phone
        public Phone Phone { get; set; } // Navigation Property

        [ForeignKey("User")]
        public int User_ID { get; set; } // Foreign Key from User
        public User User { get; set; } // Navigation Property
    }
}
