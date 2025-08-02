using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class BranchMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListBranchMapIID { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }
        [DataMember]
        public Nullable<long> BranchID { get; set; } 
        [DataMember]
        public string BranchName { get; set; }

    }
}
