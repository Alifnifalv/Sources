using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("Subjects", Schema = "schools")]
    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            OnlineExamResultSubjectMaps = new HashSet<OnlineExamResultSubjectMap>();
            OnlineExamSubjectMaps = new HashSet<OnlineExamSubjectMap>();
            QuestionGroups = new HashSet<QuestionGroup>();
            Questions = new HashSet<Question>();
            //Questions1 = new HashSet<Question>();
            //Students = new HashSet<Student>();
            //Students1 = new HashSet<Student>();
            //Students2 = new HashSet<Student>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectID { get; set; }

        public byte? SubjectTypeID { get; set; }

        [StringLength(500)]
        public string SubjectName { get; set; }

        [StringLength(20)]
        public string HexCodeUpper { get; set; }

        [StringLength(20)]
        public string HexCodeLower { get; set; }

        public bool? IsLanguage { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(50)]
        public string SubjectCode { get; set; }

        [StringLength(500)]
        public string SubjectText { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamResultSubjectMap> OnlineExamResultSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamSubjectMap> OnlineExamSubjectMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionGroup> QuestionGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Question> Questions1 { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students2 { get; set; }
    }
}