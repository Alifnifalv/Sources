using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Account
    {
        public Account()
        {
            this.Accounts1 = new List<Account>();
            this.AccountTransactionDetails = new List<AccountTransactionDetail>();
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
            this.AccountTransactions = new List<AccountTransaction>();
            this.Assets = new List<Asset>();
            this.Assets1 = new List<Asset>();
            this.Assets2 = new List<Asset>();
            this.AssetTransactionDetails = new List<AssetTransactionDetail>();
            this.ChartOfAccountMaps = new List<ChartOfAccountMap>();
            this.CustomerAccountMaps = new List<CustomerAccountMap>();
            this.Payables = new List<Payable>();
            this.Receivables = new List<Receivable>();
            this.SupplierAccountMaps = new List<SupplierAccountMap>();
        }

        public long AccountID { get; set; }
        public string Alias { get; set; }
        public string AccountName { get; set; }
        public Nullable<long> ParentAccountID { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<byte> AccountBehavoirID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string ChildAliasPrefix { get; set; }
        public Nullable<long> ChildLastID { get; set; }
        public virtual AccountBehavoir AccountBehavoir { get; set; }
        public virtual ICollection<Account> Accounts1 { get; set; }
        public virtual Account Account1 { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Asset> Assets1 { get; set; }
        public virtual ICollection<Asset> Assets2 { get; set; }
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
    }
}
