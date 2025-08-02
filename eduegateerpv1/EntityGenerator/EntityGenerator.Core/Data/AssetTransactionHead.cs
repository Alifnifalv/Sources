using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetTransactionHead", Schema = "asset")]
    public partial class AssetTransactionHead
    {
        public AssetTransactionHead()
        {
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
            AssetTransactionHeadAccountMaps = new HashSet<AssetTransactionHeadAccountMap>();
        }

        [Key]
        public long HeadIID { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public long? DocumentStatusID { get; set; }
        public byte? ProcessingStatusID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("DocumentStatusID")]
        [InverseProperty("AssetTransactionHeads")]
        public virtual DocumentReferenceStatusMap DocumentStatus { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("AssetTransactionHeads")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("ProcessingStatusID")]
        [InverseProperty("AssetTransactionHeads")]
        public virtual TransactionStatus ProcessingStatus { get; set; }
        [InverseProperty("Head")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        [InverseProperty("AssetTransactionHead")]
        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
    }
}
