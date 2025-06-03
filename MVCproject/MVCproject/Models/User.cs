using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCproject.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; } // Primary Key

        [Required]
        [MaxLength(50)]
        public string First_Name { get; set; } // First Name

        [Required]
        [MaxLength(50)]
        public string Last_Name { get; set; } // Last Name

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } // Username

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } // Password

        [Required]
        [MaxLength(50)]
        public string Property { get; set; } // Property

        public ICollection<Client> Clients { get; set; } // Navigation Property
    }
}
