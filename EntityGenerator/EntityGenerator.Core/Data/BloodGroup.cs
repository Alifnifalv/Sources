using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BloodGroups", Schema = "mutual")]
    public partial class BloodGroup
    {
        public BloodGroup()
        {
            Employees = new HashSet<Employee>();
            JobSeekers = new HashSet<JobSeeker>();
            MemberHealths = new HashSet<MemberHealth>();
            Students = new HashSet<Student>();
        }

        [Key]
        public int BloodGroupID { get; set; }
        [StringLength(100)]
        public string BloodGroupName { get; set; }

        [InverseProperty("BloodGroup")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("BloodGroup")]
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
        [InverseProperty("BloodGroup")]
        public virtual ICollection<MemberHealth> MemberHealths { get; set; }
        [InverseProperty("BloodGroup")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
