namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.OnlineExamResults")]
    public partial class OnlineExamResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnlineExamResult()
        {
            OnlineExamResultQuestionMaps = new HashSet<OnlineExamResultQuestionMap>();
            OnlineExamResultSubjectMaps = new HashSet<OnlineExamResultSubjectMap>();
        }

        [Key]
        public long OnlineExamResultIID { get; set; }

        public decimal? Marks { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? CandidateID { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public long? OnlineExamID { get; set; }

        public byte? ResultStatusID { get; set; }

        public virtual Candidate Candidate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamResultQuestionMap> OnlineExamResultQuestionMaps { get; set; }

        public virtual OnlineExam OnlineExam { get; set; }

        public virtual OnlineExamResultStatus OnlineExamResultStatus { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnlineExamResultSubjectMap> OnlineExamResultSubjectMaps { get; set; }
    }
}
