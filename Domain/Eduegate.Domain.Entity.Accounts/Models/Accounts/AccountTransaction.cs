using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Assets;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("AccountTransactions", Schema = "account")]
    [Index("AccountID", Name = "INDX_ACC_TRN_ACCOUNTID")]
    [Index("TransactionDate", Name = "INDX_ACC_TRN_TRNDATE")]
    public partial class AccountTransaction
    {
        public AccountTransaction()
        {
            AccountTransactionHeadAccountMaps = new HashSet<AccountTransactionHeadAccountMap>();
            AssetTransactionHeadAccountMaps = new HashSet<AssetTransactionHeadAccountMap>();
        }

        [Key]
        public long TransactionIID { get; set; }

        public int? DocumentTypeID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNumber { get; set; }

        public long? AccountID { get; set; }

        public decimal? Amount { get; set; }

        public decimal? InclusiveTaxAmount { get; set; }

        public decimal? ExclusiveTaxAmount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public bool? DebitOrCredit { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int? CostCenterID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public int? BudgetID { get; set; }

        public virtual Account Account { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }

        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
    }
}