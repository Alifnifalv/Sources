namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("asset.AssetTransactionDetails")]
    public partial class AssetTransactionDetail
    {
        [Key]
        public long DetailIID { get; set; }

        public long? HeadID { get; set; }

        public long? AssetID { get; set; }

        public DateTime? StartDate { get; set; }

        public int? Quantity { get; set; }

        public decimal? Amount { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? AccountID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetTransactionHead AssetTransactionHead { get; set; }
    }
}
