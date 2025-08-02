namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("asset.AssetTransactionHeadAccountMaps")]
    public partial class AssetTransactionHeadAccountMap
    {
        [Key]
        public long AssetTransactionHeadAccountMapIID { get; set; }

        public long AccountTransactionID { get; set; }

        public long AssetTransactionHeadID { get; set; }

        public virtual AccountTransaction AccountTransaction { get; set; }

        public virtual AssetTransactionHead AssetTransactionHead { get; set; }
    }
}
