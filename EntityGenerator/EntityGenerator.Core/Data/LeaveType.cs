using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveTypes", Schema = "payroll")]
    public partial class LeaveType
    {
        public LeaveType()
        {
            EmployeeLeaveAllocations = new HashSet<EmployeeLeaveAllocation>();
            EmployeePromotionLeaveAllocations = new HashSet<EmployeePromotionLeaveAllocation>();
            LeaveApplications = new HashSet<LeaveApplication>();
        }

        [Key]
        public int LeaveTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? MaxDaysAllowed { get; set; }
        public bool? IsCarryForward { get; set; }
        public bool? IsLeaveWithoutPay { get; set; }
        public bool? AllowNegativeBalance { get; set; }
        public bool? IncludeHolidayWithinLeaves { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("LeaveType")]
        public virtual ICollection<EmployeeLeaveAllocation> EmployeeLeaveAllocations { get; set; }
        [InverseProperty("LeaveType")]
        public virtual ICollection<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }
        [InverseProperty("LeaveType")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
    }
}
