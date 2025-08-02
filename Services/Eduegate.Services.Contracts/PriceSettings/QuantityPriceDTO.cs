using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class QuantityPriceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListSKUQuantityMapIID { get; set; }

        [DataMember]
        public Nullable<long> ProductPriceListSKUMapID { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }

        [DataMember]
        public Nullable<decimal> Price { get; set; }

        [DataMember]
        public Nullable<decimal> PricePercentage { get; set; }

        [DataMember]
        public Nullable<decimal> Discount { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
    }
}
