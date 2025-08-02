using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimesheetTimeTypes", Schema = "payroll")]
    public partial class TimesheetTimeType
    {
        public TimesheetTimeType()
        {
            EmployeeTimeSheetApprovals = new HashSet<EmployeeTimeSheetApproval>();
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
        }

        [Key]
        public byte TimesheetTimeTypeID { get; set; }
        [StringLength(50)]
        public string TypeNameEn { get; set; }
        [StringLength(50)]
        public string TypeNameAr { get; set; }

        [InverseProperty("TimesheetTimeType")]
        public virtual ICollection<EmployeeTimeSheetApproval> EmployeeTimeSheetApprovals { get; set; }
        [InverseProperty("TimesheetTimeType")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
    }
}
