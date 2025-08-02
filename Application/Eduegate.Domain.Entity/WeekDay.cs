namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.WeekDays")]
    public partial class WeekDay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WeekDay()
        {
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableLogs = new HashSet<TimeTableLog>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WeekDayID { get; set; }

        public int? ClassTimingSetID { get; set; }

        public byte? DayID { get; set; }

        public bool? IsWeekDay { get; set; }

        public virtual Day Day { get; set; }

        public virtual ClassTimingSet ClassTimingSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
    }
}
