using Eduegate.Domain.Entity.HR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("EmployeeTimeSheetApprovals", Schema = "payroll")]
    public partial class EmployeeTimeSheetApproval
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeTimeSheetApproval()
        {
            EmployeeTimesheetApprovalMaps = new HashSet<EmployeeTimesheetApprovalMap>();
        }

        [Key]
        public long EmployeeTimeSheetApprovalIID { get; set; }

        public long EmployeeID { get; set; }

        public DateTime? TimesheetDateFrom { get; set; }

        public DateTime? TimesheetDateTo { get; set; }

        public byte? TimesheetTimeTypeID { get; set; }

        public decimal? OTHours { get; set; }

        public decimal? NormalHours { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? TimesheetApprovalStatusID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TimesheetApprovalStatus TimesheetApprovalStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimesheetApprovalMap> EmployeeTimesheetApprovalMaps { get; set; }

        public virtual TimesheetTimeType TimesheetTimeType { get; set; }
    }
}