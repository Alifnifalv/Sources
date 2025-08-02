namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.TimesheetApprovalStatuses")]
    public partial class TimesheetApprovalStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimesheetApprovalStatus()
        {
            EmployeeTimeSheetApprovals = new HashSet<EmployeeTimeSheetApproval>();
        }

        public byte TimesheetApprovalStatusID { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimeSheetApproval> EmployeeTimeSheetApprovals { get; set; }
    }
}
