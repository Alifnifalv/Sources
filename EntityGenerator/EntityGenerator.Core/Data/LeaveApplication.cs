using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveApplications", Schema = "payroll")]
    public partial class LeaveApplication
    {
        public LeaveApplication()
        {
            LeaveApplicationApprovers = new HashSet<LeaveApplicationApprover>();
        }

        [Key]
        public long LeaveApplicationIID { get; set; }
        public int? CompanyID { get; set; }
        public int? LeaveTypeID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public byte? FromSessionID { get; set; }
        public byte? ToSessionID { get; set; }
        [StringLength(500)]
        public string OtherReason { get; set; }
        public byte? LeaveStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? StaffLeaveReasonID { get; set; }
        public bool? IsLeaveWithoutPay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RejoiningDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpectedRejoiningDate { get; set; }
        public bool? IsHalfDay { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("LeaveApplications")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("FromSessionID")]
        [InverseProperty("LeaveApplicationFromSessions")]
        public virtual LeaveSession FromSession { get; set; }
        [ForeignKey("LeaveStatusID")]
        [InverseProperty("LeaveApplications")]
        public virtual LeaveStatus LeaveStatus { get; set; }
        [ForeignKey("LeaveTypeID")]
        [InverseProperty("LeaveApplications")]
        public virtual LeaveType LeaveType { get; set; }
        [ForeignKey("StaffLeaveReasonID")]
        [InverseProperty("LeaveApplications")]
        public virtual StaffLeaveReason StaffLeaveReason { get; set; }
        [ForeignKey("ToSessionID")]
        [InverseProperty("LeaveApplicationToSessions")]
        public virtual LeaveSession ToSession { get; set; }
        [InverseProperty("LeaveApplication")]
        public virtual ICollection<LeaveApplicationApprover> LeaveApplicationApprovers { get; set; }
    }
}
