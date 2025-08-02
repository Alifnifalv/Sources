using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TimeTableView
    {
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        public int ClassTimingID { get; set; }
        [StringLength(50)]
        public string TimingDescription { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int WeekDayID { get; set; }
        public byte? DayID { get; set; }
        [StringLength(50)]
        public string DayName { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public long EmployeeIID { get; set; }
        [Required]
        [StringLength(555)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string TimingSetName { get; set; }
        [StringLength(100)]
        public string TimeTableDescription { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
