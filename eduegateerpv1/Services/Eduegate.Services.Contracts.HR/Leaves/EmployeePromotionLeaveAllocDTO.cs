using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class EmployeePromotionLeaveAllocDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeePromotionLeaveAllocationIID { get; set; }

        [DataMember]
        public long? EmployeePromotionID { get; set; }

        [DataMember]
        public int? LeaveTypeID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public double? AllocatedLeaves { get; set; }
        [DataMember]
        public KeyValueDTO LeaveType { get; set; }
    }
}
