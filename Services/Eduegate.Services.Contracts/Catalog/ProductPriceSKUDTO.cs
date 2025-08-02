using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductPriceSKUDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListItemMapIID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public Nullable<long> ProductPriceListID { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUID { get; set; }

        [DataMember]
        public long? CustomerGroupID { get; set; }

        [DataMember]
        public Nullable<long> UnitGroundID { get; set; }

        [DataMember]
        public Nullable<decimal> SellingQuantityLimit { get; set; }

        [DataMember]
        public Nullable<decimal> Amount { get; set; }

        [DataMember]
        public string ProductPriceSKU { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public Nullable<byte> SortOrder { get; set; }

        [DataMember]
        public string PartNumber { get; set; }

        [DataMember]
        public string Barcode { get; set; }

        [DataMember]
        public Nullable<decimal> PricePercentage { get; set; }

        [DataMember]
        public Nullable<decimal> Price { get; set; }

        [DataMember]
        public Nullable<decimal> Discount { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }

        [DataMember]
        public QuantityPriceDTO PriceDetails { get; set; }

        [DataMember]
        public List<QuantityPriceDTO> ProductSKUQuntityLevelPrices { get; set; }

        [DataMember]
        public string PriceDescription { get; set; }

        [DataMember]
        public Nullable<decimal> Cost { get; set; }

        [DataMember]
        public bool IsMarketPlace { get; set; }

        [DataMember]
        public Nullable<bool> IsActive { get; set; }
        [DataMember]
        public KeyValueDTO PriceListStatus { get; set; }
    }    
}
