using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveStatuses", Schema = "payroll")]
    public partial class LeaveStatus
    {
        public LeaveStatus()
        {
            LeaveApplications = new HashSet<LeaveApplication>();
        }

        [Key]
        public byte LeaveStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("LeaveStatus")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
    }
}
