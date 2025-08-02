namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.TimesheetTimeTypes")]
    public partial class TimesheetTimeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimesheetTimeType()
        {
            EmployeeTimeSheetApprovals = new HashSet<EmployeeTimeSheetApproval>();
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
        }

        public byte TimesheetTimeTypeID { get; set; }

        [StringLength(50)]
        public string TypeNameEn { get; set; }

        [StringLength(50)]
        public string TypeNameAr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimeSheetApproval> EmployeeTimeSheetApprovals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
    }
}
