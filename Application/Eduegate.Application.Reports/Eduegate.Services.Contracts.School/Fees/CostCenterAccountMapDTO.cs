using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
   public class CostCenterAccountMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CostCenterAccountMapIID { get; set; }

        [DataMember]
        public int? CostCenterID { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public bool? IsAffect_A { get; set; } = false;
        [DataMember]
        public bool? IsAffect_L { get; set; } = false;
        [DataMember]
        public bool? IsAffect_C { get; set; } = false;
        [DataMember]
        public bool? IsAffect_E { get; set; } = false;
        [DataMember]
        public bool? IsAffect_I { get; set; } = false;
    }
}
