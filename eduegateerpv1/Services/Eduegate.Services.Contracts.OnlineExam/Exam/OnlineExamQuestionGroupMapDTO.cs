using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class OnlineExamQuestionGroupMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long ExamQuestionGroupMapIID { get; set; }

        [DataMember]
        public long? OnlineExamID { get; set; }

        [DataMember]
        public int? QuestionGroupID { get; set; }

        [DataMember]
        public long? NumberOfQuestions { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public decimal? MaximumMarks { get; set; }

        [DataMember]
        public long? GroupTotalQnCount { get; set; }

    }
}