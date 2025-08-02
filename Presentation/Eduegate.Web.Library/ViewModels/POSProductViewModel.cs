using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class POSProductViewModel
    {
        public long ProductIID { get; set; }
        public long ProductSKUMapIID { get; set; }
        public decimal ProductPrice { get; set; }
        public Nullable<int> Sequence { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string PartNo { get; set; }
        public string SKU { get; set; }
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
    }
}