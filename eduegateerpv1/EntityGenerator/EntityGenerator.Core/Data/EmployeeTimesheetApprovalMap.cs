using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("EmployeeTimesheetApprovalMaps", Schema = "payroll")]
    public partial class EmployeeTimesheetApprovalMap
    {
        public long EmployeeTimesheetApprovalMapIID { get; set; }
        public long? EmployeeTimesheetApprovalID { get; set; }
        public long? EmployeeTimeSheetID { get; set; }

        [ForeignKey("EmployeeTimeSheetID")]
        public virtual EmployeeTimeSheet EmployeeTimeSheet { get; set; }
        [ForeignKey("EmployeeTimesheetApprovalID")]
        public virtual EmployeeTimeSheetApproval EmployeeTimesheetApproval { get; set; }
    }
}
