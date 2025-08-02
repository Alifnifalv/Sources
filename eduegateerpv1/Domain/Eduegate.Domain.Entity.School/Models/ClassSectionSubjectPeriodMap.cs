using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ClassSectionSubjectPeriodMaps", Schema = "schools")]
    public partial class ClassSectionSubjectPeriodMap
    {
        [Key]
        public long PeriodMapIID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? SubjectTypeID { get; set; }

        public int? SubjectID { get; set; }

        public int? TotalPeriods { get; set; }

        public int? WeekPeriods { get; set; }

        public int? MinimumPeriods { get; set; }

        public int? MaximumPeriods { get; set; }

        public int? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Required]
        //public byte[] TimeStamps { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
