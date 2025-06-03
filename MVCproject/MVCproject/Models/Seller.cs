using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class Seller
    {
        [Key]
        public int Seller_ID { get; set; } // Primary Key

        [ForeignKey("User")]
        public int User_ID { get; set; } // Foreign Key from User
        public User User { get; set; } // Navigation Property
    }
}
