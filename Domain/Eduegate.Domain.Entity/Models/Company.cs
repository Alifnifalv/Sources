using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Companies", Schema = "mutual")]
    public partial class Company
    {
        public Company()
        {
            this.ClaimLoginMaps = new List<ClaimLoginMap>();
            this.ClaimSetLoginMaps = new List<ClaimSetLoginMap>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.ProductInventories = new List<ProductInventory>();
            this.ProductInventorySerialMaps = new List<ProductInventorySerialMap>();
            this.ProductTypeDeliveryTypeMaps = new List<ProductTypeDeliveryTypeMap>();
            this.TransactionHeads = new List<TransactionHead>();
            this.CompanyCurrencyMaps = new List<CompanyCurrencyMap>();
            this.Currencies = new List<Currency>();
            this.Departments = new List<Department>();
            PeriodClosingTranHeads = new HashSet<PeriodClosingTranHead>();
        }

        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> BaseCurrencyID { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string RegistraionNo { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Language Language { get; set; }
        public virtual CompanyStatus CompanyStatuses { get; set; } 
        public virtual ICollection<CompanyCurrencyMap> CompanyCurrencyMaps { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<ProductInventoryConfig> ProductInventoryConfigs { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<PeriodClosingTranHead> PeriodClosingTranHeads { get; set; }
    }
}
