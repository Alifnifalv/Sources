using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Chapters", Schema = "schools")]
    public partial class Chapter
    {
        public Chapter()
        {
            SubjectUnits = new HashSet<SubjectUnit>();
        }
        [Key]
        public long ChapterIID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? SubjectID { get; set; }

        public string ChapterTitle { get; set; }

        public string Description { get; set; }

        public long? TotalPeriods { get; set; }

        public long? TotalHours { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<SubjectUnit> SubjectUnits { get; set; }

    }
}