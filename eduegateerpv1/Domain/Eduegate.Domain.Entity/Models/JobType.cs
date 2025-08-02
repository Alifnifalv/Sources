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
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int JobTypeID { get; set; }
        [StringLength(50)]
        public string JobTypeName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
