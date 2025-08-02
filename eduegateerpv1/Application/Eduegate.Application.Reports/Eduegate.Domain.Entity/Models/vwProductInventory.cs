using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductInventory
    {
        public int ProductID { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductPartNo { get; set; }
        public Nullable<System.DateTime> ProductCreatedOn { get; set; }
        public string ProductGroup { get; set; }
        public string ProductName { get; set; }
        public string BrandNameEn { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public int ProductAvailableQuantity { get; set; }
        public Nullable<int> ordercnt { get; set; }
        public Nullable<int> itemsold { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public bool ProductActive { get; set; }
        public Nullable<long> ProductWeight { get; set; }
        public string ProductCategoryAll { get; set; }
        public int ProductReOrderLevel { get; set; }
        public int SupplierID { get; set; }
        public long ProductManagerID { get; set; }
    }
}
