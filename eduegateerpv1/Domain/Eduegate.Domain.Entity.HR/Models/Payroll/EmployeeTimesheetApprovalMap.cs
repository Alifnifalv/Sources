using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("EmployeeTimesheetApprovalMaps", Schema = "payroll")]
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