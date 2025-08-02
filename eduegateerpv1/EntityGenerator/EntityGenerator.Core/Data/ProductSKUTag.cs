using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSKUTags", Schema = "catalog")]
    [Index("TagName", Name = "UQ_TagName", IsUnique = true)]
    public partial class ProductSKUTag
    {
        public ProductSKUTag()
        {
            ProductSKUTagMaps = new HashSet<ProductSKUTagMap>();
        }

        [Key]
        public long ProductSKUTagIID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("ProductSKUTag")]
        public virtual ICollection<ProductSKUTagMap> ProductSKUTagMaps { get; set; }
    }
}
