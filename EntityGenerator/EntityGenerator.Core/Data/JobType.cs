using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobTypes", Schema = "payroll")]
    public partial class JobType
    {
        public JobType()
        {
            AvailableJobs = new HashSet<AvailableJob>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int JobTypeID { get; set; }
        [StringLength(50)]
        public string JobTypeName { get; set; }

        [InverseProperty("JobType")]
        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }
        [InverseProperty("JobType")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
