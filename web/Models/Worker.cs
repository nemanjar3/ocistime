using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Import this namespace


namespace web.Models
{
    [Table("Worker")] // Map to the 'Jobs' table in the database
    public class Worker
    {
        [Key]
        public int WorkerID { get; set; }
        public string? LastName { get; set; }
        public string? FirstMidName { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }
        public int JobID { get; set; }

        // Navigation property for the Jobs table
        public Job? Job { get; set; }

        // Uncomment and modify according to your related entities
        // public ICollection<Enrollment> Enrollments { get; set; }
    }
}
