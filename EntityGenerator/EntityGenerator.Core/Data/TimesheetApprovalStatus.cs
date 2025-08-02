using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimesheetApprovalStatuses", Schema = "payroll")]
    public partial class TimesheetApprovalStatus
    {
        public TimesheetApprovalStatus()
        {
            EmployeeTimeSheetApprovals = new HashSet<EmployeeTimeSheetApproval>();
        }

        [Key]
        public byte TimesheetApprovalStatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        [InverseProperty("TimesheetApprovalStatus")]
        public virtual ICollection<EmployeeTimeSheetApproval> EmployeeTimeSheetApprovals { get; set; }
    }
}
