using System;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int WorkerID { get; set; }
        public string UserID { get; set; }
        public DateTime BookingDate { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
