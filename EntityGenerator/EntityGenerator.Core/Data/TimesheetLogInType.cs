using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimesheetLogInTypes", Schema = "payroll")]
    public partial class TimesheetLogInType
    {
        public TimesheetLogInType()
        {
            TimesheetEntryLogs = new HashSet<TimesheetEntryLog>();
        }

        [Key]
        public short LogInTypeID { get; set; }
        [StringLength(100)]
        public string LogInTypeName { get; set; }

        [InverseProperty("LogInType")]
        public virtual ICollection<TimesheetEntryLog> TimesheetEntryLogs { get; set; }
    }
}
