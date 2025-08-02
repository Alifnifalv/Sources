namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Accounts", Schema = "account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Accounts1 = new HashSet<Account>();
            //AccountTaxTransactions = new HashSet<AccountTaxTransaction>();
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            AccountTransactions = new HashSet<AccountTransaction>();
            Assets = new HashSet<Asset>();
            Assets1 = new HashSet<Asset>();
            Assets2 = new HashSet<Asset>();
            AssetTransactionDetails = new HashSet<AssetTransactionDetail>();
            //ChartOfAccountMaps = new HashSet<ChartOfAccountMap>();
            //CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            //EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            FeeMasters = new HashSet<FeeMaster>();
            FeeMasters1 = new HashSet<FeeMaster>();
            FeeMasters2 = new HashSet<FeeMaster>();
            FeeMasters3 = new HashSet<FeeMaster>();
            FeeMasters4 = new HashSet<FeeMaster>();
            FeeMasters5 = new HashSet<FeeMaster>();
            FeeMasters6 = new HashSet<FeeMaster>();
            FeeMasters7 = new HashSet<FeeMaster>();
            FineMasters = new HashSet<FineMaster>();
            //Payables = new HashSet<Payable>();
            PaymentModes = new HashSet<PaymentMode>();
            PackageConfigs = new HashSet<PackageConfig>();
            //Receivables = new HashSet<Receivable>();
            //SalaryStructures = new HashSet<SalaryStructure>();
            //SupplierAccountMaps = new HashSet<SupplierAccountMap>();
            //Taxes = new HashSet<Tax>();
            //TaxTemplateItems = new HashSet<TaxTemplateItem>();
            //TaxTransactions = new HashSet<TaxTransaction>();
        }

        [Key]

        public long AccountID { get; set; }

        [StringLength(30)]
        public string Alias { get; set; }

        [StringLength(50)]
        public string AccountName { get; set; }

        public long? ParentAccountID { get; set; }

        public int? GroupID { get; set; }

        public byte? AccountBehavoirID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(100)]
        public string ChildAliasPrefix { get; set; }

        public long? ChildLastID { get; set; }

        [StringLength(50)]
        public string ExternalReferenceID { get; set; }

        [StringLength(30)]
        public string AccountCode { get; set; }

        [StringLength(500)]
        public string AccountAddress { get; set; }

        [StringLength(50)]
        public string TaxRegistrationNum { get; set; }

        public bool? IsEnableSubLedger { get; set; }

        public virtual AccountBehavoir AccountBehavoir { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts1 { get; set; }

        public virtual Account Account1 { get; set; }

        public virtual Group Group { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AccountTaxTransaction> AccountTaxTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asset> Assets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asset> Assets1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asset> Assets2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetTransactionDetail> AssetTransactionDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Payable> Payables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentMode> PaymentModes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FineMaster> FineMasters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters5 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters6 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeMaster> FeeMasters7 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageConfig> PackageConfigs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Receivable> Receivables { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Tax> Taxes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TaxTemplateItem> TaxTemplateItems { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TaxTransaction> TaxTransactions { get; set; }
    }
}
