using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListBrandMaps", Schema = "catalog")]
    public partial class ProductPriceListBrandMap
    {
        [Key]
        public long ProductPriceListBrandMapIID { get; set; }
        public long? ProductPriceListID { get; set; }
        public long? BrandID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPercentage { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("ProductPriceListBrandMaps")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("ProductPriceListID")]
        [InverseProperty("ProductPriceListBrandMaps")]
        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
