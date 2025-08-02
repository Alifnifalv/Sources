using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AccountTransaction
    {
        public AccountTransaction()
        {
            this.AccountTransactionHeadAccountMaps = new List<AccountTransactionHeadAccountMap>();
            this.AssetTransactionHeadAccountMaps = new List<AssetTransactionHeadAccountMap>();
            this.TransactionHeadAccountMaps = new List<TransactionHeadAccountMap>();
        }

        public long TransactionIID { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? BudgetID { get; set; }
        public string TransactionNumber { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> InclusiveTaxAmount { get; set; }
        public Nullable<decimal> ExclusiveTaxAmount { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }

        public Nullable<bool> DebitOrCredit { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual Account Account { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
        public virtual ICollection<TransactionHeadAccountMap> TransactionHeadAccountMaps { get; set; }
        public virtual ICollection<AccountTransactionInventoryHeadMap> AccountTransactionInventoryHeadMaps { get; set; }
    }
}
