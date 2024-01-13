using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Import this namespace

namespace web.Models
{
    [Table("Jobs")] // Map to the 'Jobs' table in the database
    public class Job
    {
        [Key]
        public int JobID { get; set; }

        [Required]
        [StringLength(100)]
        public string JobName { get; set; }

        public ICollection<Worker> Workers { get; set; }

        public Job()
        {
            Workers = new HashSet<Worker>();
        }
    }
}
