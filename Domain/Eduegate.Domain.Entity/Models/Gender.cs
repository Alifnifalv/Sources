using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Genders", Schema = "mutual")]
    public partial class Gender
    {
        public Gender()
        {
            this.Employees = new List<Employee>();
            JobSeekers = new HashSet<JobSeeker>();
        }

        [Key]
        public byte GenderID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
    }
}
