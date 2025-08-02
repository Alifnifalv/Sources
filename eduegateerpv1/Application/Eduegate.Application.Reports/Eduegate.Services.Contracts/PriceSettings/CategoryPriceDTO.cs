using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CategoryPriceDTO
    {
        [DataMember]
        public long ProductPriceListCategoryMapIID { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }
        [DataMember]
        public Nullable<long> CategoryID { get; set; }
        [DataMember]
        public QuantityPriceDTO PriceDetails { get; set; }
        
    }
}
