using System;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.CustomEntity
{
    public partial class ProductPriceSKU
    {
        [Key]
        public long ProductPriceListItemMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
        public Nullable<long> UnitGroundID { get; set; }
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> PricePercentage { get; set;}
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public string PartNumber { get; set; }
        public string Barcode { get; set; }

    }
}