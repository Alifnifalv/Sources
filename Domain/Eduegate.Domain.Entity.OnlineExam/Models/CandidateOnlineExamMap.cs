using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("CandidateOnlineExamMaps", Schema = "exam")]
    public partial class CandidateOnlineExamMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CandidateOnlineExamMap()
        {
            CandidateAnswers = new HashSet<CandidateAnswer>();
            CandidateAssesments = new HashSet<CandidateAssesment>();
        }

        [Key]
        public long CandidateOnlinExamMapIID { get; set; }

        public long? CandidateID { get; set; }

        public long? OnlineExamID { get; set; }

        public double? Duration { get; set; }

        public double? AdditionalTime { get; set; }

        public byte? OnlineExamStatusID { get; set; }

        public byte? OnlineExamOperationStatusID { get; set; }

        public DateTime? ExamStartTime { get; set; }

        public DateTime? ExamEndTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateAssesment> CandidateAssesments { get; set; }

        public virtual OnlineExamOperationStatus OnlineExamOperationStatus { get; set; }

        public virtual OnlineExamStatus OnlineExamStatus { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual OnlineExam OnlineExam { get; set; }
    }
}