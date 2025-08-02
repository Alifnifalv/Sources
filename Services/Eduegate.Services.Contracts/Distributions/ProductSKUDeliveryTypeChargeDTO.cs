using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class ProductSKUDeliveryTypeChargeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductDeliveryTypeMapIID { get; set; }
        [DataMember]
        public int DeliveryTypeID { get; set; }
        [DataMember]
        public Nullable<long> ProductID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }
        [DataMember]
        public Nullable<decimal> DeliveryCharge { get; set; }
        [DataMember]
        public Nullable<decimal> DeliveryChargePercentage { get; set; }
    }
}
