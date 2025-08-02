namespace Eduegate.Domain.Entity.HR.Models
{
    using Eduegate.Domain.Entity.HR.Payroll;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Companies", Schema = "mutual")]
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            //ClaimLoginMaps = new HashSet<ClaimLoginMap>();
            //ClaimSetLoginMaps = new HashSet<ClaimSetLoginMap>();
            //ProductInventoryConfigs = new HashSet<ProductInventoryConfig>();
            //ProductPriceListProductMaps = new HashSet<ProductPriceListProductMap>();
            //ProductPriceListSKUMaps = new HashSet<ProductPriceListSKUMap>();
            //Sites = new HashSet<Site>();
            //OrderDeliveryHolidayHeads = new HashSet<OrderDeliveryHolidayHead>();
            //InvetoryTransactions = new HashSet<InvetoryTransaction>();
            //ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            //ProductInventories = new HashSet<ProductInventory>();
            //ProductInventorySerialMaps = new HashSet<ProductInventorySerialMap>();
            //ProductTypeDeliveryTypeMaps = new HashSet<ProductTypeDeliveryTypeMap>();
            //TransactionHeads = new HashSet<TransactionHead>();
            //Attendences = new HashSet<Attendence>();
            //CompanyCurrencyMaps = new HashSet<CompanyCurrencyMap>();
            //Currencies = new HashSet<Currency>();
            Employees = new HashSet<Employee>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
            //Schools = new HashSet<School>();
            //Settings = new HashSet<Setting>();
            //Suppliers = new HashSet<Supplier>();
            //UserSettings = new HashSet<UserSetting>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [StringLength(100)]
        public string CompanyCode { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        public int? CompanyGroupID { get; set; }

        public int? CountryID { get; set; }

        public int? BaseCurrencyID { get; set; }

        public int? LanguageID { get; set; }

        [StringLength(50)]
        public string RegistraionNo { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? StatusID { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductInventoryConfig> ProductInventoryConfigs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Site> Sites { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductInventory> ProductInventories { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Attendence> Attendences { get; set; }

        public virtual Country Country { get; set; }

        //public virtual CompanyGroup CompanyGroup { get; set; }

        //public virtual CompanyStatus CompanyStatus { get; set; }

        //public virtual Currency Currency { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Currency> Currencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<School> Schools { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Setting> Settings { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Supplier> Suppliers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<UserSetting> UserSettings { get; set; }
    }
}
