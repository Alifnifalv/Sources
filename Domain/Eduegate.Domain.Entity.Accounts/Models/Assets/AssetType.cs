using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("AssetTypes", Schema = "asset")]
    public partial class AssetType
    {
        public AssetType()
        {
            Assets = new HashSet<Asset>();
        }

        [Key]
        public int AssetTypeID { get; set; }

        [StringLength(100)]
        public string AssetTypeName { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}