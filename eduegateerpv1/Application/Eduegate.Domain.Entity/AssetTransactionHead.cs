namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("asset.AssetTransactionHead")]
    public partial class AssetTransactionHead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AssetTransactionHead()
        {
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
            AssetTransactionHeadAccountMaps = new HashSet<AssetTransactionHeadAccountMap>();
        }

        [Key]
        public long HeadIID { get; set; }

        public int? DocumentTypeID { get; set; }

        public DateTime? EntryDate { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public long? DocumentStatusID { get; set; }

        public byte? ProcessingStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }

        public virtual DocumentReferenceStatusMap DocumentReferenceStatusMap { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
    }
}
