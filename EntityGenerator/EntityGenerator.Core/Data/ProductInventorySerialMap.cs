using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductInventorySerialMaps", Schema = "inventory")]
    public partial class ProductInventorySerialMap
    {
        public long ProductSKUMapID { get; set; }
        public long Batch { get; set; }
        public int? CompanyID { get; set; }
        public long BranchID { get; set; }
        [StringLength(200)]
        public string SerialNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? Used { get; set; }
        [Key]
        public long ProductInventorySerialMapIID { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("ProductInventorySerialMaps")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("ProductInventorySerialMaps")]
        public virtual Company Company { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductInventorySerialMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
