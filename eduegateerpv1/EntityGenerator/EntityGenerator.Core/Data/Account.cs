using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
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

        [ForeignKey("AccountBehavoirID")]
        [InverseProperty("Accounts")]
        public virtual AccountBehavoir AccountBehavoir { get; set; }
        [ForeignKey("GroupID")]
        [InverseProperty("Accounts")]
        public virtual Group Group { get; set; }
        [ForeignKey("ParentAccountID")]
        [InverseProperty("InverseParentAccount")]
        public virtual Account ParentAccount { get; set; }
        [InverseProperty("Accound")]
        public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
        [InverseProperty("AccumulatedDepGLAcc")]
        public virtual ICollection<Asset> AssetAccumulatedDepGLAccs { get; set; }
        [InverseProperty("AssetGlAcc")]
        public virtual ICollection<Asset> AssetAssetGlAccs { get; set; }
        [InverseProperty("DepreciationExpGLAcc")]
        public virtual ICollection<Asset> AssetDepreciationExpGLAccs { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }
        [InverseProperty("AdvanceAccount")]
        public virtual ICollection<FeeMaster> FeeMasterAdvanceAccounts { get; set; }
        [InverseProperty("AdvanceTaxAccount")]
        public virtual ICollection<FeeMaster> FeeMasterAdvanceTaxAccounts { get; set; }
        [InverseProperty("LedgerAccount")]
        public virtual ICollection<FeeMaster> FeeMasterLedgerAccounts { get; set; }
        [InverseProperty("OSTaxAccount")]
        public virtual ICollection<FeeMaster> FeeMasterOSTaxAccounts { get; set; }
        [InverseProperty("OutstandingAccount")]
        public virtual ICollection<FeeMaster> FeeMasterOutstandingAccounts { get; set; }
        [InverseProperty("ProvisionforAdvanceAccount")]
        public virtual ICollection<FeeMaster> FeeMasterProvisionforAdvanceAccounts { get; set; }
        [InverseProperty("ProvisionforOutstandingAccount")]
        public virtual ICollection<FeeMaster> FeeMasterProvisionforOutstandingAccounts { get; set; }
        [InverseProperty("TaxLedgerAccount")]
        public virtual ICollection<FeeMaster> FeeMasterTaxLedgerAccounts { get; set; }
        [InverseProperty("LedgerAccount")]
        public virtual ICollection<FineMaster> FineMasters { get; set; }
        [InverseProperty("ParentAccount")]
        public virtual ICollection<Account> InverseParentAccount { get; set; }
        [InverseProperty("CreditNoteAccount")]
        public virtual ICollection<PackageConfig> PackageConfigs { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<Payable> Payables { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<PaymentMode> PaymentModes { get; set; }
        [InverseProperty("GLAccount")]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<Receivable> Receivables { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }
        [InverseProperty("Accound")]
        public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
        [InverseProperty("Account")]
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
