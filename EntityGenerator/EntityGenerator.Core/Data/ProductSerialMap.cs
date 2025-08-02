using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSerialMaps", Schema = "inventory")]
    [Index("SerialNo", Name = "uc_SerialNumber", IsUnique = true)]
    public partial class ProductSerialMap
    {
        [Key]
        public long ProductSerialIID { get; set; }
        [StringLength(200)]
        public string SerialNo { get; set; }
        public long? DetailID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? ProductSKUMapID { get; set; }

        [ForeignKey("DetailID")]
        [InverseProperty("ProductSerialMaps")]
        public virtual TransactionDetail Detail { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductSerialMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
