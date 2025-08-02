using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
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

        public decimal? Marks { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public long? CandidateID { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public long? OnlineExamID { get; set; }

        public byte? ResultStatusID { get; set; }
        
        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Candidate Candidate { get; set; }
        
        public virtual OnlineExam OnlineExam { get; set; }
        
        public virtual OnlineExamResultStatus ResultStatus { get; set; }
        
        public virtual School School { get; set; }
        
        public virtual ICollection<OnlineExamResultQuestionMap> OnlineExamResultQuestionMaps { get; set; }
        
        public virtual ICollection<OnlineExamResultSubjectMap> OnlineExamResultSubjectMaps { get; set; }
    }
}