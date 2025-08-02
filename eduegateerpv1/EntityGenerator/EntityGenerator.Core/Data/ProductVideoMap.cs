using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductVideoMaps", Schema = "catalog")]
    public partial class ProductVideoMap
    {
        [Key]
        public long ProductVideoMapIID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [StringLength(500)]
        public string VideoFile { get; set; }
        public byte? Sequence { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductVideoMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductVideoMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
