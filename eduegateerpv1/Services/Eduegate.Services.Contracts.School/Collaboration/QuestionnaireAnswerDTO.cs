using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class QuestionnaireAnswerDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  QuestionnaireAnswerIID { get; set; }
        [DataMember]
        public int?  QuestionnaireAnswerTypeID { get; set; }
        [DataMember]
        public long?  QuestionnaireID { get; set; }
        [DataMember]
        public string  Answer { get; set; }
        [DataMember]
        public string  MoreInfo { get; set; }
    }
}


