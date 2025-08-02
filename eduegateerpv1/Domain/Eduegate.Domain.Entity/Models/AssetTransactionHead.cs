using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AssetTransactionHead", Schema = "asset")]
    public partial class AssetTransactionHead
    {
        public AssetTransactionHead()
        {
            this.AssetTransactionDetails = new List<AssetTransactionDetail>();
            this.AssetTransactionHeadAccountMaps = new List<AssetTransactionHeadAccountMap>();
        }

        [Key]
        public long HeadIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<byte> ProcessingStatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        public virtual DocumentReferenceStatusMap DocumentReferenceStatusMap { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
    }
}
