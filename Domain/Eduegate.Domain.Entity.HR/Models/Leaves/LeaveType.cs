namespace Eduegate.Domain.Entity.HR.Leaves
{
    using Eduegate.Domain.Entity.HR.Models.Leaves;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LeaveTypes", Schema = "payroll")]
    public partial class LeaveType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveType()
        {
            LeaveApplications = new HashSet<LeaveApplication>();
            EmployeeLeaveAllocations = new HashSet<EmployeeLeaveAllocation>();
            EmployeePromotionLeaveAllocations = new HashSet<EmployeePromotionLeaveAllocation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeaveAllocation> EmployeeLeaveAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }
    }
}
