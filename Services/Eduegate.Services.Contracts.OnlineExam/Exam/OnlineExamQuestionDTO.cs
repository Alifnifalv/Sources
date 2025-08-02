using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class OnlineExamQuestionDTO : BaseMasterDTO
    {
        public OnlineExamQuestionDTO()
        {
            QuestionOptionMaps = new List<QuestionOptionMapDTO>();
            QuestionListMap = new List<OnlineExamQuestionDTO>();
            PassageQuestions = new List<OnlineExamQuestionDTO>();
        }

        [DataMember]
        public long OnlineExamQuestionIID { get; set; }

        [DataMember]
        public long? CandidateID { get; set; }

        [DataMember]
        public string CandidateName { get; set; }

        [DataMember]
        public long? OnlineExamID { get; set; }

        [DataMember]
        [StringLength(500)]
        public string ExamName { get; set; }

        [DataMember]
        [StringLength(500)]
        public string ExamDescription { get; set; }

        [DataMember]
        [StringLength(500)]
        public string GroupName { get; set; }

        [DataMember]
        public long? QuestionID { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        [StringLength(50)]
        public string AnswerType { get; set; }

        [DataMember]
        public long? QuestionOptionCount { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public List<QuestionOptionMapDTO> QuestionOptionMaps { get; set; }

        [DataMember]
        public double? ExamMaximumDuration { get; set; }

        [DataMember]
        public string QuestionAnswer { get; set; }

        [DataMember]
        public string QuestionGroup { get; set; }

        [DataMember]
        public decimal? Points { get; set; }


        //for listMap
        [DataMember]
        public List<OnlineExamQuestionDTO> QuestionListMap { get; set; }

        [DataMember]
        public string PassageQuestion { get; set; }

        [DataMember]
        public long? PassageQuestionID { get; set; }

        [DataMember]
        public bool IsPassageQn { get; set; }

        [DataMember]
        public List<OnlineExamQuestionDTO> PassageQuestions { get; set; }

        [DataMember]
        public string DocFile { get; set; }
    }
}