using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Assets;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("Companies", Schema = "mutual")]
    public partial class Company
    {
        public Company()
        {
            AssetInventories = new HashSet<AssetInventory>();
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
            Currencies = new HashSet<Currency>();
            Suppliers = new HashSet<Supplier>();
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

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

        public byte? StatusID { get; set; }

        public virtual Currency BaseCurrency { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<AssetInventory> AssetInventories { get; set; }

        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }

        public virtual ICollection<Currency> Currencies { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}