using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WeekDays", Schema = "schools")]
    public partial class WeekDay
    {
        public WeekDay()
        {
            TimeTableAllocations = new HashSet<TimeTableAllocation>();
            TimeTableLogs = new HashSet<TimeTableLog>();
        }

        [Key]
        public int WeekDayID { get; set; }
        public int? ClassTimingSetID { get; set; }
        public byte? DayID { get; set; }
        public bool? IsWeekDay { get; set; }

        [ForeignKey("ClassTimingSetID")]
        [InverseProperty("WeekDays")]
        public virtual ClassTimingSet ClassTimingSet { get; set; }
        [ForeignKey("DayID")]
        [InverseProperty("WeekDays")]
        public virtual Day Day { get; set; }
        [InverseProperty("WeekDay")]
        public virtual ICollection<TimeTableAllocation> TimeTableAllocations { get; set; }
        [InverseProperty("WeekDay")]
        public virtual ICollection<TimeTableLog> TimeTableLogs { get; set; }
    }
}
