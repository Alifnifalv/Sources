using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimesheetEntryStatuses", Schema = "payroll")]
    public partial class TimesheetEntryStatus
    {
        public TimesheetEntryStatus()
        {
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
        }

        [Key]
        public byte TimesheetEntryStatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        [InverseProperty("TimesheetEntryStatus")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
    }
}
