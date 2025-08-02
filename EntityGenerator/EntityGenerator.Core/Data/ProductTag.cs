using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductTags", Schema = "catalog")]
    public partial class ProductTag
    {
        public ProductTag()
        {
            ProductTagMaps = new HashSet<ProductTagMap>();
        }

        [Key]
        public long ProductTagIID { get; set; }
        public long? ProductID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductTags")]
        public virtual Product Product { get; set; }
        [InverseProperty("ProductTag")]
        public virtual ICollection<ProductTagMap> ProductTagMaps { get; set; }
    }
}
