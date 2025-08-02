using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMasterIntl
    {
        public int ProductMasterIntlID { get; set; }
        public int RefProductMasterID { get; set; }
        public short RefCountryID { get; set; }
        public string ProductType { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public int ProductAvailableQty { get; set; }
        public bool ProductActive { get; set; }
        public bool ProductActiveCountry { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<decimal> ProductDiscountPercent { get; set; }
        public Nullable<bool> MultiPrice { get; set; }
        public Nullable<bool> MultiPriceIgnore { get; set; }
        public Nullable<bool> IsProductVoucher { get; set; }
        public Nullable<bool> QuantityDiscount { get; set; }
        public Nullable<bool> IntlShipping { get; set; }
        public Nullable<decimal> NextDayDeliveryCost { get; set; }
        public Nullable<decimal> ProductCostPrice { get; set; }
        public string Location { get; set; }
        public Nullable<int> RefSupplierID { get; set; }
        public Nullable<bool> KwtInventory { get; set; }
    }
}
