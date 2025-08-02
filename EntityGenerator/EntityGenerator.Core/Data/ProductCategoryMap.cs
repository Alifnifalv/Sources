using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductCategoryMaps", Schema = "catalog")]
    [Index("ProductID", Name = "IDX_ProductCategoryMaps_ProductID_CategoryID")]
    public partial class ProductCategoryMap
    {
        public ProductCategoryMap()
        {
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
        }

        [Key]
        public long ProductCategoryMapIID { get; set; }
        public long? ProductID { get; set; }
        public long? CategoryID { get; set; }
        public bool? IsPrimary { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("ProductCategoryMaps")]
        public virtual Category Category { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductCategoryMaps")]
        public virtual Product Product { get; set; }
        [InverseProperty("ProductCategoryMap")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }
    }
}
