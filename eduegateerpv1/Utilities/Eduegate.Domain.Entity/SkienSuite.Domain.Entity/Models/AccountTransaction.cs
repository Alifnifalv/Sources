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
        }

        public long TransactionIID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> DebitOrCredit { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<AccountTransactionHeadAccountMap> AccountTransactionHeadAccountMaps { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual ICollection<AssetTransactionHeadAccountMap> AssetTransactionHeadAccountMaps { get; set; }
    }
}
