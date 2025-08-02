namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeTimeSheets")]
    public partial class EmployeeTimeSheet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeTimeSheet()
        {
            EmployeeTimesheetApprovalMaps = new HashSet<EmployeeTimesheetApprovalMap>();
        }

        [Key]
        public long EmployeeTimeSheetIID { get; set; }

        public long EmployeeID { get; set; }

        public long TaskID { get; set; }

        [Column(TypeName = "date")]
        public DateTime TimesheetDate { get; set; }

        public decimal? OTHours { get; set; }

        public decimal? NormalHours { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? TimesheetEntryStatusID { get; set; }

        public TimeSpan? FromTime { get; set; }

        public TimeSpan? ToTime { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public byte? TimesheetTimeTypeID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TimesheetEntryStatus TimesheetEntryStatus { get; set; }

        public virtual TimesheetTimeType TimesheetTimeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimesheetApprovalMap> EmployeeTimesheetApprovalMaps { get; set; }

        public virtual Task Task { get; set; }
    }
}
