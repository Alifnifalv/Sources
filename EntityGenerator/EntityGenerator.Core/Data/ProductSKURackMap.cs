using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSKURackMaps", Schema = "catalog")]
    public partial class ProductSKURackMap
    {
        [Key]
        public long ProductSKURackMapIID { get; set; }
        public long RackID { get; set; }
        public long ProductSKUMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long ProductID { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductSKURackMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductSKURackMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        [ForeignKey("RackID")]
        [InverseProperty("ProductSKURackMaps")]
        public virtual Rack Rack { get; set; }
    }
}
