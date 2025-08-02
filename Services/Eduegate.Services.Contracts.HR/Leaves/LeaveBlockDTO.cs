using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    public class LeaveBlockDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    
    {
        [DataMember]
        public long LeaveBlockListIID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; }

        //[DataMember]
        //public int? CreatedBy { get; set; }

        //[DataMember]
        //public int? UpdatedBy { get; set; }

        //[DataMember]
        //public DateTime? CreatedDate { get; set; }

        //[DataMember]
        //public DateTime? UpdatedDate { get; set; }

    }
}
