using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductLocationMaps", Schema = "inventory")]
    public partial class ProductLocationMap
    {
        [Key]
        public long ProductLocationMapIID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public long? LocationID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LocationID")]
        [InverseProperty("ProductLocationMaps")]
        public virtual Location Location { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductLocationMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductLocationMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
