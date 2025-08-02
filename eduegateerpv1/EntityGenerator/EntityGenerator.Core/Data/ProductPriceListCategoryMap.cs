using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListCategoryMaps", Schema = "catalog")]
    public partial class ProductPriceListCategoryMap
    {
        [Key]
        public long ProductPriceListCategoryMapIID { get; set; }
        public long? ProductPriceListID { get; set; }
        public long? CategoryID { get; set; }
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

        [ForeignKey("CategoryID")]
        [InverseProperty("ProductPriceListCategoryMaps")]
        public virtual Category Category { get; set; }
        [ForeignKey("ProductPriceListID")]
        [InverseProperty("ProductPriceListCategoryMaps")]
        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
