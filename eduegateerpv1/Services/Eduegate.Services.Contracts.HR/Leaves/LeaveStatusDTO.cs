using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class LeaveStatusDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte LeaveStatusID { get; set; }
        [DataMember]
        public string StatusName { get; set; }
    }
}
