namespace Eduegate.Domain.Entity.School.Models
{
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
            LeaveAllocations = new HashSet<LeaveAllocation>();
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
        public virtual ICollection<LeaveAllocation> LeaveAllocations { get; set; }
    }
}