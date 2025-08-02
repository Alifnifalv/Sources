using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimesheetEntryLogs", Schema = "payroll")]
    public partial class TimesheetEntryLog
    {
        [Key]
        public long TimesheetEntryLogID { get; set; }
        public long? EmployeeID { get; set; }
        public short? EntryTypeID { get; set; }
        public short? LogInTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDateTime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("TimesheetEntryLogs")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EntryTypeID")]
        [InverseProperty("TimesheetEntryLogs")]
        public virtual TimesheetEntryType EntryType { get; set; }
        [ForeignKey("LogInTypeID")]
        [InverseProperty("TimesheetEntryLogs")]
        public virtual TimesheetLogInType LogInType { get; set; }
    }
}
