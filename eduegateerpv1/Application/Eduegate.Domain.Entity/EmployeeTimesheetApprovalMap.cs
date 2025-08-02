namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeTimesheetApprovalMaps")]
    public partial class EmployeeTimesheetApprovalMap
    {
        [Key]
        public long EmployeeTimesheetApprovalMapIID { get; set; }

        public long? EmployeeTimesheetApprovalID { get; set; }

        public long? EmployeeTimeSheetID { get; set; }

        public virtual EmployeeTimeSheetApproval EmployeeTimeSheetApproval { get; set; }

        public virtual EmployeeTimeSheet EmployeeTimeSheet { get; set; }
    }
}
