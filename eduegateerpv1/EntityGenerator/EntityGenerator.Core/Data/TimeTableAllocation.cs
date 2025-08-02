using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTableAllocations", Schema = "schools")]
    public partial class TimeTableAllocation
    {
        [Key]
        public long TimeTableAllocationIID { get; set; }
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
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? SectionID { get; set; }
        public int? ClassId { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassId")]
        [InverseProperty("TimeTableAllocations")]
        public virtual Class Class { get; set; }
        [ForeignKey("ClassTimingID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual ClassTiming ClassTiming { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual Section Section { get; set; }
        [ForeignKey("StaffID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TimeTableID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual TimeTable TimeTable { get; set; }
        [ForeignKey("WeekDayID")]
        [InverseProperty("TimeTableAllocations")]
        public virtual WeekDay WeekDay { get; set; }
    }
}
