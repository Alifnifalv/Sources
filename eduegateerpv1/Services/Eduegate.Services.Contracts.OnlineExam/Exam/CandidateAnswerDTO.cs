using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class CandidateAnswerDTO : BaseMasterDTO
    {
        public CandidateAnswerDTO()
        {
            AnswerList = new List<CandidateAnswerDTO>();
            OnlineExamQuestionList = new List<OnlineExamQuestionDTO>();
            QuestionOptionMapIDs = new List<long?>();
    }

        [DataMember]
        public long CandidateAnswerIID { get; set; }

        [DataMember]
        public long? CandidateID { get; set; }

        [DataMember]
        public long? CandidateOnlineExamMapID { get; set; }

        [DataMember]
        public DateTime? DateOfAnswer { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public long? QuestionOptionMapID { get; set; }

        [DataMember]
        public string OtherDetails { get; set; }

        [DataMember]
        public string OtherAnswers { get; set; }

        [DataMember]
        public string CandidateName { get; set; }

        [DataMember]
        public string QuestionOptionMap { get; set; }

        [DataMember]
        public long? OnlineExamID { get; set; }

        [DataMember]
        public string OnlineExamName { get; set; }

        [DataMember]
        public long? OnlineExamQuestionID { get; set; }

        [DataMember]
        public string OnlineExamQuestion { get; set; }

        [DataMember]
        public List<long?> QuestionOptionMapIDs { get; set; }

        //for listMap
        [DataMember]
        public List<CandidateAnswerDTO> AnswerList { get; set; }

        [DataMember]
        public List<OnlineExamQuestionDTO> OnlineExamQuestionList { get; set; }
    }
}