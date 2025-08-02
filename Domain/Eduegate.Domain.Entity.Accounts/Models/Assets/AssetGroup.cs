using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}