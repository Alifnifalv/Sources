using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimesheetEntryTypes", Schema = "payroll")]
    public partial class TimesheetEntryType
    {
        public TimesheetEntryType()
        {
            TimesheetEntryLogs = new HashSet<TimesheetEntryLog>();
        }

        [Key]
        public short EntryTypeID { get; set; }
        [StringLength(100)]
        public string EntryTypeName { get; set; }

        [InverseProperty("EntryType")]
        public virtual ICollection<TimesheetEntryLog> TimesheetEntryLogs { get; set; }
    }
}
