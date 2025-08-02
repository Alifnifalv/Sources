using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamResults", Schema = "exam")]
    public partial class OnlineExamResult
    {
        public OnlineExamResult()
        {
            OnlineExamResultQuestionMaps = new HashSet<OnlineExamResultQuestionMap>();
            OnlineExamResultSubjectMaps = new HashSet<OnlineExamResultSubjectMap>();
        }

        [Key]
        public long OnlineExamResultIID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Marks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? CandidateID { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public long? OnlineExamID { get; set; }
        public byte? ResultStatusID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("OnlineExamResults")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("CandidateID")]
        [InverseProperty("OnlineExamResults")]
        public virtual Candidate Candidate { get; set; }
        [ForeignKey("OnlineExamID")]
        [InverseProperty("OnlineExamResults")]
        public virtual OnlineExam OnlineExam { get; set; }
        [ForeignKey("ResultStatusID")]
        [InverseProperty("OnlineExamResults")]
        public virtual OnlineExamResultStatus ResultStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("OnlineExamResults")]
        public virtual School School { get; set; }
        [InverseProperty("OnlineExamResult")]
        public virtual ICollection<OnlineExamResultQuestionMap> OnlineExamResultQuestionMaps { get; set; }
        [InverseProperty("OnlineExamResults")]
        public virtual ICollection<OnlineExamResultSubjectMap> OnlineExamResultSubjectMaps { get; set; }
    }
}
