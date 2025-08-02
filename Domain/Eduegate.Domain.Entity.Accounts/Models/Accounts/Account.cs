using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.Accounts.Models.Budgets;
using Eduegate.Domain.Entity.Accounts.Models.Schools;
using Eduegate.Domain.Entity.Accounts.Models.Payrolls;
using Eduegate.Domain.Entity.Accounts.Models.Inventory;
using Eduegate.Domain.Entity.Accounts.Models.Assets;


namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("Accounts", Schema = "account")]
    [Index("ExternalReferenceID", Name = "INDX_EXT_REF")]
    public partial class Account
    {
        public Account()
        {
            AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            AccountTransactions = new HashSet<AccountTransaction>();
            AssetAccumulatedDepGLAccs = new HashSet<Asset>();
            AssetAssetGlAccs = new HashSet<Asset>();
            AssetDepreciationExpGLAccs = new HashSet<Asset>();
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
            BankAccounts = new HashSet<BankAccount>();
            BudgetEntryAccountMaps = new HashSet<BudgetEntryAccountMap>();
            ChartOfAccountMaps = new HashSet<ChartOfAccountMap>();
            CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            FeeMasterAdvanceAccounts = new HashSet<FeeMaster>();
            FeeMasterAdvanceTaxAccounts = new HashSet<FeeMaster>();
            FeeMasterLedgerAccounts = new HashSet<FeeMaster>();
            FeeMasterOSTaxAccounts = new HashSet<FeeMaster>();
            FeeMasterOutstandingAccounts = new HashSet<FeeMaster>();
            FeeMasterProvisionforAdvanceAccounts = new HashSet<FeeMaster>();
            FeeMasterProvisionforOutstandingAccounts = new HashSet<FeeMaster>();
            FeeMasterTaxLedgerAccounts = new HashSet<FeeMaster>();
            FineMasters = new HashSet<FineMaster>();
            InverseParentAccount = new HashSet<Account>();
            PackageConfigs = new HashSet<PackageConfig>();
            Payables = new HashSet<Payable>();
            PaymentModes = new HashSet<PaymentMode>();
            Products = new HashSet<Product>();
            Receivables = new HashSet<Receivable>();
            SalaryStructures = new HashSet<SalaryStructure>();
            SupplierAccountMaps = new HashSet<SupplierAccountMap>();
            TaxTemplateItems = new HashSet<TaxTemplateItem>();
            TaxTransactions = new HashSet<TaxTransaction>();
            Taxes = new HashSet<Tax>();
            TransactionAllocationDetailAccounts = new HashSet<TransactionAllocationDetail>();
            TransactionAllocationDetailGL_Account = new HashSet<TransactionAllocationDetail>();
        }

        [Key]
        public long AccountID { get; set; }

        [StringLength(30)]
        [Unicode(false)]
        public string Alias { get; set; }

        [StringLength(500)]
        public string AccountName { get; set; }

        public long? ParentAccountID { get; set; }

        public int? GroupID { get; set; }

        public byte? AccountBehavoirID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        [StringLength(100)]
        public string ChildAliasPrefix { get; set; }

        public long? ChildLastID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string ExternalReferenceID { get; set; }

        [StringLength(30)]
        [Unicode(false)]
        public string AccountCode { get; set; }

        [StringLength(500)]
        [Unicode(false)]
        public string AccountAddress { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string TaxRegistrationNum { get; set; }

        public bool? IsEnableSubLedger { get; set; }

        public virtual Account ParentAccount { get; set; }

        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }

        public virtual ICollection<Asset> AssetAccumulatedDepGLAccs { get; set; }

        public virtual ICollection<Asset> AssetAssetGlAccs { get; set; }

        public virtual ICollection<Asset> AssetDepreciationExpGLAccs { get; set; }

        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        public virtual ICollection<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }

        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }

        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }

        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterAdvanceAccounts { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterAdvanceTaxAccounts { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterLedgerAccounts { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterOSTaxAccounts { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterOutstandingAccounts { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterProvisionforAdvanceAccounts { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterProvisionforOutstandingAccounts { get; set; }

        public virtual ICollection<FeeMaster> FeeMasterTaxLedgerAccounts { get; set; }

        public virtual ICollection<FineMaster> FineMasters { get; set; }

        public virtual ICollection<Account> InverseParentAccount { get; set; }

        public virtual ICollection<PackageConfig> PackageConfigs { get; set; }

        public virtual ICollection<Payable> Payables { get; set; }

        public virtual ICollection<PaymentMode> PaymentModes { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Receivable> Receivables { get; set; }

        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }

        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }

        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }

        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<TransactionAllocationDetail> TransactionAllocationDetailAccounts { get; set; }
        [InverseProperty("GL_Account")]
        public virtual ICollection<TransactionAllocationDetail> TransactionAllocationDetailGL_Account { get; set; }


    }
}