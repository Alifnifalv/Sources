using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssetInvetoryTransactions", Schema = "asset")]
    public partial class AssetInvetoryTransaction
    {
        [Key]
        public long AssetInvetoryTransactionIID { get; set; }
        public int? SerialNo { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public long? AssetID { get; set; }
        public long? AssetSerialMapID { get; set; }
        public long? BatchID { get; set; }
        public long? AccountID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OriginalQty { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public long? BranchID { get; set; }
        public int? CompanyID { get; set; }
        public long? AssetTransactionHeadID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AssetID")]
        [InverseProperty("AssetInvetoryTransactions")]
        public virtual Asset Asset { get; set; }
        [ForeignKey("AssetSerialMapID")]
        [InverseProperty("AssetInvetoryTransactions")]
        public virtual AssetSerialMap AssetSerialMap { get; set; }
        [ForeignKey("AssetTransactionHeadID")]
        [InverseProperty("AssetInvetoryTransactions")]
        public virtual AssetTransactionHead AssetTransactionHead { get; set; }
        [ForeignKey("BranchID")]
        [InverseProperty("AssetInvetoryTransactions")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("AssetInvetoryTransactions")]
        public virtual Company Company { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("AssetInvetoryTransactions")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
