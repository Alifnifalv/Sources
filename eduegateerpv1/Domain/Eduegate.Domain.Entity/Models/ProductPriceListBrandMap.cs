using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductPriceListBrandMaps", Schema = "catalog")]
    public partial class ProductPriceListBrandMap
    {
        [Key]
        public long ProductPriceListBrandMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<decimal> Price { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
