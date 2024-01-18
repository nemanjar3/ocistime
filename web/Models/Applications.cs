using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    [Table("Applications")]
    public class Application
    {
        public int? ApplicationID { get; set; } // Automatically filled by the database
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string? PhoneNumber { get; set; }
        public int JobID { get; set; } // Foreign key reference to the Jobs table

    // Navigation property for the related Job entity
    public Job Job { get; set; }
    }

}
