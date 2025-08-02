using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeTimeSheets", Schema = "payroll")]
    [Index("EmployeeID", Name = "IDX_EmployeeTimeSheets_EmployeeID_")]
    [Index("EmployeeID", "TimesheetEntryStatusID", "TimesheetDate", Name = "IDX_EmployeeTimeSheets_EmployeeID__TimesheetEntryStatusIDTimesheetDate_")]
    public partial class EmployeeTimeSheet
    {
        [Key]
        public long EmployeeTimeSheetIID { get; set; }
        public long EmployeeID { get; set; }
        public long TaskID { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimesheetDate { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? OTHours { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NormalHours { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? TimesheetEntryStatusID { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public byte? TimesheetTimeTypeID { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeTimeSheets")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("TaskID")]
        [InverseProperty("EmployeeTimeSheets")]
        public virtual Task Task { get; set; }
        [ForeignKey("TimesheetEntryStatusID")]
        [InverseProperty("EmployeeTimeSheets")]
        public virtual TimesheetEntryStatus TimesheetEntryStatus { get; set; }
        [ForeignKey("TimesheetTimeTypeID")]
        [InverseProperty("EmployeeTimeSheets")]
        public virtual TimesheetTimeType TimesheetTimeType { get; set; }
    }
}
