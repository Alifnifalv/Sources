using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AssetTransactionHeadDetailSearchView
    {
        public long HeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public long? DocumentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        public byte? ProcessingStatusID { get; set; }
        [StringLength(50)]
        public string TransactionStatusesName { get; set; }
        public long DetailIID { get; set; }
        public long? HeadID { get; set; }
        public long? AccountID { get; set; }
        public long? AssetID { get; set; }
        [StringLength(50)]
        public string AssetCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        public int? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "decimal(20, 3)")]
        public decimal? Debit { get; set; }
        public int? Createdby { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
