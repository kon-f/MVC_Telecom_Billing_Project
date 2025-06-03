using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCproject.Models
{
    public class BillCall
    {
        [Key]
        [ForeignKey("Bill")]
        public int Bill_ID { get; set; } // Foreign Key from Bill
        public Bill Bill { get; set; } // Navigation Property

        [ForeignKey("Call")]
        public int Call_ID { get; set; } // Foreign Key from Call
        public Call Call { get; set; } // Navigation Property
    }
}
