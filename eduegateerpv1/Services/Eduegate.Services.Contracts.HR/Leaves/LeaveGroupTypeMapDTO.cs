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
    public class LeaveGroupTypeMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LeaveGroupTypeMapIID { get; set; }
        [DataMember]
        public int? LeaveTypeID { get; set; }
        [DataMember]
        public int? LeaveGroupID { get; set; }
        [DataMember]
        public double? NoofLeaves { get; set; }
        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public KeyValueDTO LeaveType { get; set; }

    }
}
