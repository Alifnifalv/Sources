using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductInventoryConfigTemp
    {
        public long ProductInventoryConfigIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<decimal> NotifyQuantity { get; set; }
        public Nullable<decimal> MinimumQuantity { get; set; }
        public Nullable<decimal> MaximumQuantity { get; set; }
        public Nullable<decimal> MinimumQuanityInCart { get; set; }
        public Nullable<decimal> MaximumQuantityInCart { get; set; }
        public Nullable<bool> IsQuntityUseDecimals { get; set; }
        public Nullable<byte> BackOrderTypeID { get; set; }
        public Nullable<byte> IsStockAvailabiltiyID { get; set; }
        public string ProductWarranty { get; set; }
        public Nullable<bool> IsSerialNumber { get; set; }
        public Nullable<bool> IsSerialRequiredForPurchase { get; set; }
        public Nullable<short> DeliveryMethod { get; set; }
        public Nullable<decimal> ProductWeight { get; set; }
        public Nullable<decimal> ProductLength { get; set; }
        public Nullable<decimal> ProductWidth { get; set; }
        public Nullable<decimal> ProductHeight { get; set; }
        public Nullable<decimal> DimensionalWeight { get; set; }
        public Nullable<short> PackingTypeID { get; set; }
        public Nullable<bool> IsMarketPlace { get; set; }
        public Nullable<decimal> HSCode { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public Nullable<decimal> ProductCost { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
