using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Syllabus", Schema = "schools")]
    public partial class Syllabu
    {
        public Syllabu()
        {
            AgeCriterias = new HashSet<AgeCriteria>();
            Leads = new HashSet<Lead>();
            StudentApplicationCurriculams = new HashSet<StudentApplication>();
            StudentApplicationPreviousSchoolSyllabus = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
        }

        [Key]
        public byte SyllabusID { get; set; }
        [StringLength(50)]
        public string SyllabusDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSchoolSyllabus { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Syllabus")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Syllabus")]
        public virtual School School { get; set; }
        [InverseProperty("Curriculum")]
        public virtual ICollection<AgeCriteria> AgeCriterias { get; set; }
        [InverseProperty("Curriculam")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("Curriculam")]
        public virtual ICollection<StudentApplication> StudentApplicationCurriculams { get; set; }
        [InverseProperty("PreviousSchoolSyllabus")]
        public virtual ICollection<StudentApplication> StudentApplicationPreviousSchoolSyllabus { get; set; }
        [InverseProperty("PreviousSchoolSyllabus")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
