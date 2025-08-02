using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;

namespace Eduegate.Domain.Entity.Accounts.Models.Assets
{
    [Table("AssetInventoryTransactions", Schema = "asset")]
    public partial class AssetInventoryTransaction
    {
        [Key]
        public long AssetInvetoryTransactionIID { get; set; }

        public int? SerialNo { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        public string TransactionNo { get; set; }

        public DateTime? TransactionDate { get; set; }

        public long? AssetID { get; set; }

        public long? AssetSerialMapID { get; set; }

        public long? BatchID { get; set; }

        public long? AccountID { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? OriginalQty { get; set; }

        public decimal? Amount { get; set; }

        public long? BranchID { get; set; }

        public int? CompanyID { get; set; }

        public long? AssetTransactionHeadID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual AssetSerialMap AssetSerialMap { get; set; }

        public virtual AssetTransactionHead AssetTransactionHead { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Company Company { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}