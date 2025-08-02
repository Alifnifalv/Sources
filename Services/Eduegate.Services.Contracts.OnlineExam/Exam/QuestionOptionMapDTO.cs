using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class QuestionOptionMapDTO : BaseMasterDTO
    {
        [DataMember]
        public long QuestionOptionMapIID { get; set; }

        [DataMember]
        public string OptionText { get; set; }

        [DataMember]
        public long? QuestionID { get; set; }

        [DataMember]
        public string ImageName { get; set; }

        [DataMember]
        public long? ContentID { get; set; }

        [DataMember]
        public bool? IsSelected { get; set; }

        [DataMember]
        public bool? IsCorrectAnswer { get; set; }

        [DataMember]
        public int? OrderNo { get; set; }
    }
}