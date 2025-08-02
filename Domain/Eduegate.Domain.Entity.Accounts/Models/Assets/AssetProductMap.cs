using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Catalog;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("AssetProductMaps", Schema = "asset")]
    public partial class AssetProductMap
    {
        [Key]
        public long AssetProductMapIID { get; set; }

        public long? AssetID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}