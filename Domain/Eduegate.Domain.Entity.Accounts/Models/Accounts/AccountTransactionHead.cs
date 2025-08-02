using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Eduegate.Domain.Entity.Accounts.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("AccountTransactionHeads", Schema = "account")]
    [Index("DocumentTypeID", Name = "IDX_AccountTransactionHeads_DocumentTypeID_")]
    public partial class AccountTransactionHead
    {
        public AccountTransactionHead()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            AccountTransactionReceivablesMaps = new HashSet<AccountTransactionReceivablesMap>();
            AccountTransactionHeadAccountMaps = new HashSet<AccountTransactionHeadAccountMap>();
            TransactionAllocationDetails = new HashSet<TransactionAllocationDetail>();
        }

        [Key]
        public long AccountTransactionHeadIID { get; set; }

        public DateTime? TransactionDate { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }

        public int? DocumentTypeID { get; set; }

        public int? PaymentModeID { get; set; }

        public long? AccountID { get; set; }

        public int? CurrencyID { get; set; }

        public decimal? ExchangeRate { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        [StringLength(100)]
        public string Reference { get; set; }

        public bool? IsPrePaid { get; set; }

        public decimal? AdvanceAmount { get; set; }

        public int? CostCenterID { get; set; }

        public decimal? AmountPaid { get; set; }

        public long? DocumentStatusID { get; set; }

        public byte? TransactionStatusID { get; set; }

        public long? BranchID { get; set; }

        public long? CompanyID { get; set; }

        public long? ReceiptsID { get; set; }

        public long? PaymentsID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference1 { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference2 { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReference3 { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        [StringLength(30)]
        [Unicode(false)]
        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        public decimal? TaxAmount { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public virtual Account Account { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual TransactionStatus TransactionStatus { get; set; }

        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        
        public virtual ICollection<AccountTransactionReceivablesMap> AccountTransactionReceivablesMaps { get; set; }
        [InverseProperty("AccountTransactionHead")]
        public virtual ICollection<TransactionAllocationDetail> TransactionAllocationDetails { get; set; }
    }
}