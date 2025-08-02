using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Genders", Schema = "mutual")]
    public partial class Gender
    {
        public Gender()
        {
            Employees = new HashSet<Employee>();
            JobSeekers = new HashSet<JobSeeker>();
            Leads = new HashSet<Lead>();
            Members = new HashSet<Member>();
            StudentApplications = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
            TransportApplctnStudentMaps = new HashSet<TransportApplctnStudentMap>();
        }

        [Key]
        public byte GenderID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("Gender")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Gender")]
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
        [InverseProperty("Gender")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("Gender")]
        public virtual ICollection<Member> Members { get; set; }
        [InverseProperty("Gender")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        [InverseProperty("Gender")]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty("Gender")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
    }
}
