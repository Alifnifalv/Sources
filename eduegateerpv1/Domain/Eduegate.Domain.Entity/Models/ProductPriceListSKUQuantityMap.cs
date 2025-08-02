using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductPriceListSKUQuantityMaps", Schema = "catalog")]
    public partial class ProductPriceListSKUQuantityMap
    {
        [Key]
        public long ProductPriceListSKUQuantityMapIID { get; set; }
        public Nullable<long> ProductPriceListSKUMapID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ProductPriceListSKUMap ProductPriceListSKUMap { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
