using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class EventAudienceTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  EventAudienceTypeID { get; set; }
        [DataMember]
        public string  AudienceTypeName { get; set; }
    }
}


