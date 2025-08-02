using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class QuestionDTO : BaseMasterDTO
    {
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
    }
}