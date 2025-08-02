using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class LeaveGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int LeaveGroupID { get; set; }
        [DataMember]
        public string LeaveGroupName { get; set; }

    }
}
