using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class PollDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  PollIID { get; set; }
        [DataMember]
        public string  PollTitle { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public System.DateTime?  ExpiryDate { get; set; }
        [DataMember]
        public bool?  IsAllowOtherAnwser { get; set; }
        [DataMember]
        public bool?  IsOpenForPolling { get; set; }
    }
}


