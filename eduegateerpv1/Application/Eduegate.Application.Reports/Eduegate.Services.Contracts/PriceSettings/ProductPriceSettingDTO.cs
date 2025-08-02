using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Services.Contracts.PriceSettings;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductPriceSettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListProductMapIID { get; set; }
        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }
        [DataMember]
        public Nullable<long> ProductID { get; set; }
        [DataMember]
        public string PriceDescription { get; set; }
        [DataMember]
        public Nullable<decimal> Price { get; set; }
        [DataMember]
        public Nullable<decimal> Discount { get; set; }
        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public bool ApplyToAllSKUs { get; set; }
        [DataMember]
        public QuantityPriceDTO PriceDetails { get; set; }
        [DataMember]
        public List<ProductPriceSettingQuantityDTO> ProductQuntityLevelPrices { get; set; }
    }
}
