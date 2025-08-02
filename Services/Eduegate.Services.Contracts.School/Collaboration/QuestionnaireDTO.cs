using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class QuestionnaireDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  QuestionnaireIID { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public int?  QuestionnaireAnswerTypeID { get; set; }
        [DataMember]
        public string  MoreInfo { get; set; }
    }
}


