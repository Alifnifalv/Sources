using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [StringLength(255)]
        public string ChapterTitle { get; set; }
        public string Description { get; set; }
        public long? TotalPeriods { get; set; }
        public long? TotalHours { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Chapters")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("Chapters")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Chapters")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("Chapters")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("Chapters")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("Chapter")]
        public virtual ICollection<SubjectUnit> SubjectUnits { get; set; }
    }
}
