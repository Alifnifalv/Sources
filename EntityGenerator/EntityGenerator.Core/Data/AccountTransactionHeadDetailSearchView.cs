using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AccountTransactionHeadDetailSearchView
    {
        public long HeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public long? DocumentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }
        public int? PaymentModeID { get; set; }
        [StringLength(50)]
        public string EntitlementName { get; set; }
        public long? AccountID { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
        public int? CurrencyID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AdvanceAmount { get; set; }
        public int? CostCenterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? AmountPaid { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        public byte? TransactionStatusID { get; set; }
        [StringLength(50)]
        public string TransactionStatusesName { get; set; }
        public long AccountTransactionDetailIID { get; set; }
        public long? HeadID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? InvoiceAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PaidAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ReturnAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "decimal(20, 3)")]
        public decimal? Debit { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ReferenceNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PaymentDueDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ExchangeRate { get; set; }
        public int? Createdby { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
