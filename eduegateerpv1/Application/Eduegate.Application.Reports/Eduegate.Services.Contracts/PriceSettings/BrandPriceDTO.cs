using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class BrandPriceDTO
    {
        [DataMember]
        public long ProductPriceListBrandMapIID { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }
        [DataMember]
        public Nullable<long> BrandID { get; set; }
        [DataMember]
        public QuantityPriceDTO PriceDetails { get; set; }
        
    }
}
