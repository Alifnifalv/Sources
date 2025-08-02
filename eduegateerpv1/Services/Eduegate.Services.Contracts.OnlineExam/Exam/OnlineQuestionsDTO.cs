using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class OnlineQuestionsDTO : BaseMasterDTO
    {
        public OnlineQuestionsDTO()
        {
            QuestionOptionMaps = new List<QuestionOptionMapDTO>();
        }

        [DataMember]
        public long QuestionIID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte? AnswerTypeID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public int? QuestionGroupID { get; set; }

        [DataMember]
        public decimal? Points { get; set; }
       
        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public string QuestionGroupName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public List<QuestionOptionMapDTO> QuestionOptionMaps { get; set; }

        [DataMember]
        public byte? TextAnswerTypeID { get; set; }

        [DataMember]
        public byte? MultipleChoiceTypeID { get; set; }

        [DataMember]
        public byte? MultiSelectTypeID { get; set; }

        [DataMember]
        public string Docfile { get; set; }

        [DataMember]
        public long? ContentFileIID { get; set; }

        [DataMember]
        public long? PassageQuestionID { get; set; }

        [DataMember]
        public string PassageQuestionName { get; set; }
    }
}