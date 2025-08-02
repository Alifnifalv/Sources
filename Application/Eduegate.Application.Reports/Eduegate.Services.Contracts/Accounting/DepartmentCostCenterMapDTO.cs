using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public  class DepartmentCostCenterMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long DepartmentCostCenterMapIID { get; set; }
        [DataMember]
        public long? DepartmentID { get; set; }
        [DataMember]
        public int CostCenterID { get; set; }
       
        [DataMember]
        public virtual KeyValueDTO CostCenter { get; set; }
        [DataMember]
        public virtual KeyValueDTO Department { get; set; }
    }
}
