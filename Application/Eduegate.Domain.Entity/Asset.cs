namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("asset.Assets")]
    public partial class Asset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asset()
        {
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
        }

        [Key]
        public long AssetIID { get; set; }

        public long? AssetCategoryID { get; set; }

        [StringLength(50)]
        public string AssetCode { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public long? AssetGlAccID { get; set; }

        public long? AccumulatedDepGLAccID { get; set; }

        public long? DepreciationExpGLAccId { get; set; }

        public int? DepreciationYears { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Account Account { get; set; }

        public virtual Account Account1 { get; set; }

        public virtual Account Account2 { get; set; }

        public virtual AssetCategory AssetCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
    }
}
