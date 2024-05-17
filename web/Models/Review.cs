using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }

        [Required]
        public int WorkerID { get; set; }

        [Required]
        public string UserID { get; set; }  // Added UserID to track which user is making the review

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int Grade { get; set; }

        // Navigation properties
        public Worker? Worker { get; set; }  // Navigation property to Worker
        public ApplicationUser? User { get; set; }  // Navigation property to User
    }
}
