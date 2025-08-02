using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
   public class OnlineExamResultQuestionMapDTO : BaseMasterDTO
    {
        public OnlineExamResultQuestionMapDTO()
        {
            QuestionOptionMaps = new List<QuestionOptionMapDTO>();
        }

        [DataMember]
        public long OnlineExamResultQuestionMapIID { get; set; }

        [DataMember]
        public long? OnlineExamResultID { get; set; }

        [DataMember]
        public long? QuestionID { get; set; }

        [DataMember]
        public string Question { get; set; }

        [DataMember]
        public decimal? Mark { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        [StringLength(30)]
        public string EntryType { get; set; }

        [DataMember]
        public long? SelectedOptionID { get; set; }

        [DataMember]
        public string SelectedOption { get; set; }

        [DataMember]
        public List<QuestionOptionMapDTO> QuestionOptionMaps { get; set; }

        [DataMember]
        public decimal? TotalMarksOfQuestion { get; set; }

        [DataMember]
        public string CandidateTextAnswer { get; set; }

        [DataMember]
        public bool IsMarkEditable { get; set; }
    }
}