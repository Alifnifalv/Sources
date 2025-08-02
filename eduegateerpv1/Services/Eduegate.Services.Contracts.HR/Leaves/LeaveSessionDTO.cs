using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class LeaveSessionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte LeaveSessionID { get; set; }

        [DataMember]
        public string SesionName { get; set; }
    }
}
