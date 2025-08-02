using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CustomerGroupPriceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListCustomerGroupMapIID { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }
        [DataMember]
        public Nullable<long> CustomerGroupID { get; set; }
        [DataMember]
        public Nullable<long> ProductID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }
        [DataMember]
        public Nullable<long> CategoryID { get; set; }
        [DataMember]
        public Nullable<long> BrandID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string PriceListName { get; set; }
        [DataMember]
        public Nullable<decimal> Price { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public Nullable<decimal> PricePercentage { get; set; }
        [DataMember]
        public Nullable<decimal> Discount { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
        [DataMember]
        public QuantityPriceDTO PriceDetails { get; set; }
    }
}
