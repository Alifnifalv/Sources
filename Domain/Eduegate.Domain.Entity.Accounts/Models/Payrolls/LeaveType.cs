using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Payrolls
{
    [Table("LeaveTypes", Schema = "payroll")]
    public partial class LeaveType
    {
        public LeaveType()
        {
            EmployeePromotionLeaveAllocations = new HashSet<EmployeePromotionLeaveAllocation>();
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }
    }
}