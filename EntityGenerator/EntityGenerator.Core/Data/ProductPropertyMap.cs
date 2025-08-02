using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPropertyMaps", Schema = "catalog")]
    public partial class ProductPropertyMap
    {
        [Key]
        public long ProductPropertyMapIID { get; set; }
        public long? ProductID { get; set; }
        public byte? PropertyTypeID { get; set; }
        public long? PropertyID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductPropertyMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductPropertyMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        [ForeignKey("PropertyID")]
        [InverseProperty("ProductPropertyMaps")]
        public virtual Property Property { get; set; }
        [ForeignKey("PropertyTypeID")]
        [InverseProperty("ProductPropertyMaps")]
        public virtual PropertyType PropertyType { get; set; }
    }
}
