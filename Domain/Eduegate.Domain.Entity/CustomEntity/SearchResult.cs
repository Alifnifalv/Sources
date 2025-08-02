using Microsoft.EntityFrameworkCore;
using System;

namespace Eduegate.Domain.Entity.CustomEntity
{
    /// <summary>
    /// return Product category result for Black Pearl
    /// </summary>
    [Keyless]
    public partial class SearchResult
    {
        public long ProductIID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public string SKU { get; set; }
        public string ImageFile { get; set; }
        public Nullable<byte> Sequence { get; set; }
        public long CategoryIID { get; set; }
        public long ProductCount { get; set; }
        public string ProductSKUCode { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public string BrandName { get; set; }
        public Nullable<decimal> DiscountedPrice { get; set; }
        public long SKUID { get; set; }
        public bool HasStock { get; set; }
        public long BrandIID { get; set; }
        public string Descirption { get; set; }
        public string LogoFile { get; set; }
        public Nullable<decimal> SellingQuantityLimit { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public sbyte ProductStatus { get; set; }

    }
}