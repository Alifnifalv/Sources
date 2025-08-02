namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("WeekDays", Schema = "schools")]
    public partial class WeekDay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WeekDay()
        {
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WeekDayID { get; set; }

        public int? ClassTimingSetID { get; set; }

        public byte? DayID { get; set; }

        public bool? IsWeekDay { get; set; }

        public virtual Day Day { get; set; }

        public virtual ClassTimingSet ClassTimingSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
    }
}
