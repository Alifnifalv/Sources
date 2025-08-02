using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class QuestionnaireAnswerTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  QuestionnaireAnswerTypeID { get; set; }
        [DataMember]
        public string  TypeName { get; set; }
    }
}


