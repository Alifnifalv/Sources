namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.TimesheetEntryStatuses")]
    public partial class TimesheetEntryStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimesheetEntryStatus()
        {
            EmployeeTimeSheets = new HashSet<EmployeeTimeSheet>();
        }

        public byte TimesheetEntryStatusID { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeTimeSheet> EmployeeTimeSheets { get; set; }
    }
}
