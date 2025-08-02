using Eduegate.Domain.Entity.Models.Inventory;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Account
    {
        public Account()
        {
            this.Accounts1 = new List<Account>();
            this.AccountTransactions = new List<AccountTransaction>();
            this.AccountTransactionDetails = new List<AccountTransactionDetail>();
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
            this.Assets = new List<Asset>();
            this.Assets1 = new List<Asset>();
            this.Assets2 = new List<Asset>();
            this.AssetTransactionDetails = new List<AssetTransactionDetail>();
            this.ChartOfAccountMaps = new List<ChartOfAccountMap>();
            this.CustomerAccountMaps = new List<CustomerAccountMap>();
            this.SupplierAccountMaps = new List<SupplierAccountMap>();
            this.Payables = new List<Payable>();
            this.Receivables = new List<Receivable>();
            this.EmployeeAccountMaps = new List<EmployeeAccountMap>();
            this.Taxes = new HashSet<Tax>();
            this.TaxTemplateItems = new List<TaxTemplateItem>();
            this.TaxTransactions = new List<TaxTransaction>();
            this.AccountTaxTransactions = new List<AccountTaxTransaction>();
        }

        public long? AccountID { get; set; }
        public string Alias { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public Nullable<long> ParentAccountID { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<byte> AccountBehavoirID { get; set; }
        public string ChildAliasPrefix { get; set; }
        public Nullable<long> ChildLastID { get; set; }
        public string ExternalReferenceID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual AccountBehavoir AccountBehavoir { get; set; }
        public virtual ICollection<Account> Accounts1 { get; set; }
        public virtual Account Account1 { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Asset> Assets1 { get; set; }
        public virtual ICollection<Asset> Assets2 { get; set; }
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public virtual ICollection<Payable> Payables { get; set; }
        public virtual ICollection<Receivable> Receivables { get; set; }
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        public virtual ICollection<EmployeeAccountMap> EmployeeAccountMaps { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
