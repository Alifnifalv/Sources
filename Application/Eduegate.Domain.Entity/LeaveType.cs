namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.LeaveTypes")]
    public partial class LeaveType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveType()
        {
            EmployeeLeaveAllocations = new HashSet<EmployeeLeaveAllocation>();
            EmployeePromotionLeaveAllocations = new HashSet<EmployeePromotionLeaveAllocation>();
            LeaveApplications = new HashSet<LeaveApplication>();
            LeaveAllocations = new HashSet<LeaveAllocation>();
        }

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
        public virtual ICollection<EmployeeLeaveAllocation> EmployeeLeaveAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionLeaveAllocation> EmployeePromotionLeaveAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveAllocation> LeaveAllocations { get; set; }
    }
}
