using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string? LastName { get; set; }
        public string? FirstMidName { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }

        //dodati ICollection na usluga, istorija usluga, ocjene
        //public DateTime EnrollmentDate { get; set; }

        //public ICollection<Enrollment> Enrollments { get; set; }
    }
}