using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Companies", Schema = "mutual")]
    public partial class Company
    {
        public Company()
        {
            AssetInventories = new HashSet<AssetInventory>();
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
            Attendences = new HashSet<Attendence>();
            Budget1 = new HashSet<Budget1>();
            ClaimLoginMaps = new HashSet<ClaimLoginMap>();
            ClaimSetLoginMaps = new HashSet<ClaimSetLoginMap>();
            CompanyCurrencyMaps = new HashSet<CompanyCurrencyMap>();
            Currencies = new HashSet<Currency>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
            Employees = new HashSet<Employee>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            OrderDeliveryHolidayHeads = new HashSet<OrderDeliveryHolidayHead>();
            PeriodClosingTranHeads = new HashSet<PeriodClosingTranHead>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductInventories = new HashSet<ProductInventory>();
            ProductInventoryConfigs = new HashSet<ProductInventoryConfig>();
            ProductInventorySerialMaps = new HashSet<ProductInventorySerialMap>();
            ProductPriceListProductMaps = new HashSet<ProductPriceListProductMap>();
            ProductPriceListSKUMaps = new HashSet<ProductPriceListSKUMap>();
            ProductTypeDeliveryTypeMaps = new HashSet<ProductTypeDeliveryTypeMap>();
            Schools = new HashSet<School>();
            Settings = new HashSet<Setting>();
            Sites = new HashSet<Site>();
            Suppliers = new HashSet<Supplier>();
            TransactionHeads = new HashSet<TransactionHead>();
            UserSettings = new HashSet<UserSetting>();
        }

        [Key]
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
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? StatusID { get; set; }

        [ForeignKey("BaseCurrencyID")]
        [InverseProperty("Companies")]
        public virtual Currency BaseCurrency { get; set; }
        [ForeignKey("CompanyGroupID")]
        [InverseProperty("Companies")]
        public virtual CompanyGroup CompanyGroup { get; set; }
        [ForeignKey("CountryID")]
        [InverseProperty("Companies")]
        public virtual Country Country { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Companies")]
        public virtual CompanyStatus Status { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<AssetInventory> AssetInventories { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Attendence> Attendences { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Budget1> Budget1 { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Currency> Currencies { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        [InverseProperty("ResidencyCompany")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<PeriodClosingTranHead> PeriodClosingTranHeads { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ProductInventoryConfig> ProductInventoryConfigs { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<School> Schools { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Setting> Settings { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Site> Sites { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<UserSetting> UserSettings { get; set; }
    }
}
