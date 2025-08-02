namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LeaveApplications", Schema = "payroll")]
    public partial class LeaveApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveApplication()
        {
            LeaveApplicationApprovers = new HashSet<LeaveApplicationApprover>();
        }

        [Key]
        public long LeaveApplicationIID { get; set; }

        public int? CompanyID { get; set; }

        public int? LeaveTypeID { get; set; }

        public long? EmployeeID { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public byte? FromSessionID { get; set; }

        public byte? ToSessionID { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        public byte? LeaveStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }

        public virtual LeaveSession LeaveSession { get; set; }

        public virtual LeaveSession LeaveSession1 { get; set; }

        public virtual LeaveStatus LeaveStatus { get; set; }

        public virtual LeaveType LeaveType { get; set; }
    }
}