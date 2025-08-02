using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassTimings", Schema = "schools")]
    public partial class ClassTiming
    {
        public ClassTiming()
        {
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableExtClassTimings = new HashSet<TimeTableExtClassTiming>();
            TimeTableLogs = new HashSet<TimeTableLog>();
        }

        [Key]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassTimings")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("BreakTypeID")]
        [InverseProperty("ClassTimings")]
        public virtual BreakType BreakType { get; set; }
        [ForeignKey("ClassTimingSetID")]
        [InverseProperty("ClassTimings")]
        public virtual ClassTimingSet ClassTimingSet { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassTimings")]
        public virtual School School { get; set; }
        [InverseProperty("ClassTiming")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("ClassTiming")]
        public virtual ICollection<TimeTableExtClassTiming> TimeTableExtClassTimings { get; set; }
        [InverseProperty("ClassTiming")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
    }
}
