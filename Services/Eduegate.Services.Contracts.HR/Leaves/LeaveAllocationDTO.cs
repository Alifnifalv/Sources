using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    public class LeaveAllocationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LeaveAllocationIID { get; set; }

        [DataMember]
        public int? LeaveTypeID { get; set; }

        [DataMember]
        public int? LeaveGroupID { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public double? AllocatedLeaves { get; set; }
        
    }
}
