using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class CandidateOnlineExamMapDTO : BaseMasterDTO
    {
        public CandidateOnlineExamMapDTO()
        {
            OnlineExamDTO = new OnlineExamsDTO();
        }

        [DataMember]
        public long? CandidateOnlinExamMapIID { get; set; }

        [DataMember]
        public long? CandidateID { get; set; }

        [DataMember]
        public long? OnlineExamID { get; set; }

        [DataMember]
        public double? Duration { get; set; }

        [DataMember]
        public double? AdditionalTime { get; set; }

        [DataMember]
        public double? OldAdditionalTime { get; set; }

        [DataMember]
        public byte? OnlineExamStatusID { get; set; }

        [DataMember]
        public byte? OnlineExamOperationStatusID { get; set; }

        [DataMember]
        public string OnlineExamName { get; set; }

        [DataMember]
        public string OnlineExamDescription { get; set; }

        [DataMember]
        public string OnlineExamStatusName { get; set; }

        [DataMember]
        public string OnlineExamOperationStatusName { get; set; }

        [DataMember]
        public OnlineExamsDTO OnlineExamDTO { get; set; }

        [DataMember]
        public bool? IsCandidateConductedExam { get; set; }

        [DataMember]
        public DateTime? ExamStartTime { get; set; }

        [DataMember]
        public DateTime? ExamEndTime { get; set; }

        [DataMember]
        public List<long> ExamQnIDs { get; set; }

        [DataMember]
        public decimal? CandidateExamQuestionsMarks { get; set; }

        [DataMember]
        public int? TotalQuestions { get; set; }
    }
}