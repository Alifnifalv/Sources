namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ScheduleLogStatuses")]
    public partial class ScheduleLogStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScheduleLogStatus()
        {
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScheduleLogStatusID { get; set; }

        [StringLength(250)]
        public string StatusNameEn { get; set; }

        [StringLength(250)]
        public string StatusNameAr { get; set; }

        [StringLength(100)]
        public string StatusTitleEn { get; set; }

        [StringLength(100)]
        public string StatusTitleAr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
    }
}
