using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeTimeSheetApprovals", Schema = "payroll")]
    public partial class EmployeeTimeSheetApproval
    {
        [Key]
        public long EmployeeTimeSheetApprovalIID { get; set; }
        public long EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimesheetDateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimesheetDateTo { get; set; }
        public byte? TimesheetTimeTypeID { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? OTHours { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NormalHours { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? TimesheetApprovalStatusID { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeTimeSheetApprovals")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("TimesheetApprovalStatusID")]
        [InverseProperty("EmployeeTimeSheetApprovals")]
        public virtual TimesheetApprovalStatus TimesheetApprovalStatus { get; set; }
        [ForeignKey("TimesheetTimeTypeID")]
        [InverseProperty("EmployeeTimeSheetApprovals")]
        public virtual TimesheetTimeType TimesheetTimeType { get; set; }
    }
}
