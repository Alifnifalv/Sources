using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CandidateOnlineExamMaps", Schema = "exam")]
    [Index("CandidateID", Name = "IDX_CandidateOnlineExamMaps_CandidateID_")]
    [Index("OnlineExamOperationStatusID", Name = "IDX_CandidateOnlineExamMaps_OnlineExamOperationStatusID_CandidateID__OnlineExamID__OnlineExamStatus")]
    public partial class CandidateOnlineExamMap
    {
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
        [Column(TypeName = "datetime")]
        public DateTime? ExamStartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExamEndTime { get; set; }

        [ForeignKey("CandidateID")]
        [InverseProperty("CandidateOnlineExamMaps")]
        public virtual Candidate Candidate { get; set; }
        [ForeignKey("OnlineExamID")]
        [InverseProperty("CandidateOnlineExamMaps")]
        public virtual OnlineExam OnlineExam { get; set; }
        [ForeignKey("OnlineExamOperationStatusID")]
        [InverseProperty("CandidateOnlineExamMaps")]
        public virtual OnlineExamOperationStatus OnlineExamOperationStatus { get; set; }
        [ForeignKey("OnlineExamStatusID")]
        [InverseProperty("CandidateOnlineExamMaps")]
        public virtual OnlineExamStatus OnlineExamStatus { get; set; }
        [InverseProperty("CandidateOnlineExamMap")]
        public virtual ICollection<CandidateAnswer> CandidateAnswers { get; set; }
        [InverseProperty("CandidateOnlinExamMap")]
        public virtual ICollection<CandidateAssesment> CandidateAssesments { get; set; }
    }
}
