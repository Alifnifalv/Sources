using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ClassSubjectSkillGroupMaps", Schema = "schools")]
    public partial class ClassSubjectSkillGroupMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassSubjectSkillGroupMap()
        {
            ClassSubjectSkillGroupSkillMaps = new HashSet<ClassSubjectSkillGroupSkillMap>();
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
            ExamSkillMaps = new HashSet<ExamSkillMap>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ClassSubjectSkillGroupMapID { get; set; }

        public long? ExamID { get; set; }

        public int? ClassID { get; set; }

        public int? SubjectID { get; set; }

        public int? MarkGradeID { get; set; }

        public decimal? TotalMarks { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public decimal? MinimumMarks { get; set; }

        public decimal? MaximumMarks { get; set; }

        public string Description { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? ISScholastic { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual MarkGrade MarkGrade { get; set; }

        public virtual Schools School { get; set; }

        public virtual Subject Subject { get; set; }

        public string ProgressCardHeader { get; set; }
        
        public decimal? ConversionFactor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassSubjectSkillGroupSkillMap> ClassSubjectSkillGroupSkillMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamSkillMap> ExamSkillMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SkillGroupSubjectMap> SkillGroupSubjectMaps { get; set; }
    }
}
