using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class QuestionnaireSetDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  QuestionnaireSetID { get; set; }
        [DataMember]
        public string  QuestionnaireSetName { get; set; }
    }
}


