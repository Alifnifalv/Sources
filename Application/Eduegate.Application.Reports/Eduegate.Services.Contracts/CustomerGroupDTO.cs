using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CustomerGroupDTO : BaseMasterDTO
    {
        [DataMember]
        public long CustomerGroupIID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public Nullable<decimal> PointLimit { get; set; }
        [DataMember]
        public List<CustomerGroupPriceDTO> CustomerGroupPrices { get; set; }
    }
}
