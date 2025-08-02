using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Company
    {
        public Company()
        {
            this.ClaimLoginMaps = new List<ClaimLoginMap>();
            this.ClaimSetLoginMaps = new List<ClaimSetLoginMap>();
            this.ProductInventoryConfigs = new List<ProductInventoryConfig>();
            this.ProductPriceListProductMaps = new List<ProductPriceListProductMap>();
            this.ProductPriceListSKUMaps = new List<ProductPriceListSKUMap>();
            this.Sites = new List<Site>();
            this.OrderDeliveryHolidayHeads = new List<OrderDeliveryHolidayHead>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.ProductDeliveryTypeMaps = new List<ProductDeliveryTypeMap>();
            this.ProductInventories = new List<ProductInventory>();
            this.ProductInventorySerialMaps = new List<ProductInventorySerialMap>();
            this.ProductTypeDeliveryTypeMaps = new List<ProductTypeDeliveryTypeMap>();
            this.TransactionHeads = new List<TransactionHead>();
            this.CompanyCurrencyMaps = new List<CompanyCurrencyMap>();
            this.Currencies = new List<Currency>();
            this.Departments = new List<Department>();
            this.Employees = new List<Employee>();
            this.Settings = new List<Setting>();
            this.Suppliers = new List<Supplier>();
        }

        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> BaseCurrencyID { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public string RegistraionNo { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        public virtual ICollection<ProductInventoryConfig> ProductInventoryConfigs { get; set; }
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<OrderDeliveryHolidayHead> OrderDeliveryHolidayHeads { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual Country Country { get; set; }
        public virtual CompanyStatus CompanyStatus { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
