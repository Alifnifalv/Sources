using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetGroups", Schema = "asset")]
    public partial class AssetGroup
    {
        public AssetGroup()
        {
            Assets = new HashSet<Asset>();
        }

        [Key]
        public int AssetGroupID { get; set; }
        [StringLength(100)]
        public string AssetGroupName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("AssetGroup")]
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
