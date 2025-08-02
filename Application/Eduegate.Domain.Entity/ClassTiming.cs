namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassTimings")]
    public partial class ClassTiming
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassTiming()
        {
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableLogs = new HashSet<TimeTableLog>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassTimingID { get; set; }

        public int? ClassTimingSetID { get; set; }

        [StringLength(50)]
        public string TimingDescription { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool? IsBreakTime { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? BreakTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual BreakType BreakType { get; set; }

        public virtual ClassTimingSet ClassTimingSet { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
    }
}
