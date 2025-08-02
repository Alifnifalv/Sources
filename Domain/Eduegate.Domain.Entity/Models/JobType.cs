using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
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
        public string JobTypeName { get; set; }
        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
