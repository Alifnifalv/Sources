using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTableLogs", Schema = "schools")]
    public partial class TimeTableLog
    {
        [Key]
        public long TimeTableLogID { get; set; }
        public int? TimeTableID { get; set; }
        public int? WeekDayID { get; set; }
        public int? ClassTimingID { get; set; }
        public int? SubjectID { get; set; }
        public long? StaffID { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AllocatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? SectionID { get; set; }
        public int? ClassId { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsEnteredManually { get; set; }
        public bool? IsReAssigned { get; set; }
        public bool? IsVirtual { get; set; }
        public bool? IsGrouped { get; set; }
        public long? TimeTableAllocationID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("TimeTableLogs")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassId")]
        [InverseProperty("TimeTableLogs")]
        public virtual Class Class { get; set; }
        [ForeignKey("ClassTimingID")]
        [InverseProperty("TimeTableLogs")]
        public virtual ClassTiming ClassTiming { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("TimeTableLogs")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("TimeTableLogs")]
        public virtual Section Section { get; set; }
        [ForeignKey("StaffID")]
        [InverseProperty("TimeTableLogs")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("TimeTableLogs")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TimeTableID")]
        [InverseProperty("TimeTableLogs")]
        public virtual TimeTable TimeTable { get; set; }
        [ForeignKey("TimeTableAllocationID")]
        [InverseProperty("TimeTableLogs")]
        public virtual TimeTableAllocation TimeTableAllocation { get; set; }
        [ForeignKey("WeekDayID")]
        [InverseProperty("TimeTableLogs")]
        public virtual WeekDay WeekDay { get; set; }
    }
}
