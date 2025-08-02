using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class CustomerGroupDeliveryChargeDTO : BaseMasterDTO
    {
        [DataMember]
        public long CustomerGroupDeliveryTypeMapIID { get; set; }
        [DataMember]
        public Nullable<int> DeliveryTypeID { get; set; }
        [DataMember]
        public Nullable<long> CustomerGroupID { get; set; }
        [DataMember]
        public Nullable<decimal> CartTotalFrom { get; set; }
        [DataMember]
        public Nullable<decimal> CartTotalTo { get; set; }
        [DataMember]
        public Nullable<decimal> DeliveryCharge { get; set; }
        [DataMember]
        public Nullable<decimal> DeliveryChargePercentage { get; set; }
        [DataMember]
        public Nullable<bool> IsDeliveryAvailable { get; set; }
    }
}
