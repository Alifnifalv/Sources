using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StaffLeaveReasons", Schema = "payroll")]
    public partial class StaffLeaveReason
    {
        public StaffLeaveReason()
        {
            LeaveApplications = new HashSet<LeaveApplication>();
        }

        [Key]
        public int StaffLeaveReasonIID { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }

        [InverseProperty("StaffLeaveReason")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
    }
}
