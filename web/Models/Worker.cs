using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public string? PhoneNumber { get; set; }
        public int JobID { get; set; }

        // Navigation property for the Jobs table
        public Job? Job { get; set; }

    }
}
