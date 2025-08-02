using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetProductMaps", Schema = "asset")]
    public partial class AssetProductMap
    {
        [Key]
        public long AssetProductMapIID { get; set; }
        public long? AssetID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AssetID")]
        [InverseProperty("AssetProductMaps")]
        public virtual Asset Asset { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("AssetProductMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
