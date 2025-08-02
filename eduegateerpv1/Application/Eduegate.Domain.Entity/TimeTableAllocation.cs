namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.TimeTableAllocations")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? SectionID { get; set; }

        public int? ClassId { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual ClassTiming ClassTiming { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual TimeTable TimeTable { get; set; }

        public virtual WeekDay WeekDay { get; set; }
    }
}
