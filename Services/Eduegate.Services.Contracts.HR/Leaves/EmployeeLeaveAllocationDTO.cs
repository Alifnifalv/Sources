using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class EmployeeLeaveAllocationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LeaveAllocationIID { get; set; }

        [DataMember]
        public int? LeaveTypeID { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public double? AllocatedLeaves { get; set; }
        [DataMember]
        public KeyValueDTO Employee { get; set; }
        [DataMember]
        public KeyValueDTO LeaveType { get; set; }
        [DataMember]
        public int? LeaveGroupID { get; set; }
        [DataMember]
        public KeyValueDTO LeaveGroup { get; set; }
    }
}
