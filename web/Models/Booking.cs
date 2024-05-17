using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int WorkerID { get; set; }

        [Required]
        public DateTime BookingDate { get; set; } 

        [Required]
        public TimeSpan BookingTime { get; set; } 

        [Required]
        public string PaymentMethod { get; set; }  

        // Navigation properties
        public Worker Worker { get; set; }
        public ApplicationUser User { get; set; }
    }
}
