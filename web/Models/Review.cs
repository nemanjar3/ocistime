using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey("Worker")]
        public int WorkerID { get; set; } 

        [Required]
        [MaxLength]
        public string Description { get; set; }

        [Required]
        public int Grade { get; set; }

        // Navigation property to User/Worker
        public Worker Worker { get; set; } // Rename to Worker if that's the actual entity
    }
}
