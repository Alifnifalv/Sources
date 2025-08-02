using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("Branches", Schema = "mutual")]
    public partial class Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branch()
        {
            //AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            //ProductPriceListBranchMaps = new HashSet<ProductPriceListBranchMap>();
            //BranchDocumentTypeMaps = new HashSet<BranchDocumentTypeMap>();
            //InventoryVerifications = new HashSet<InventoryVerification>();
            //InvetoryTransactions = new HashSet<InvetoryTransaction>();
            //Locations = new HashSet<Location>();
            //ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            //ProductInventories = new HashSet<ProductInventory>();
            //ProductInventorySerialMaps = new HashSet<ProductInventorySerialMap>();
            //TransactionHeads = new HashSet<TransactionHead>();
            //TransactionHeads1 = new HashSet<TransactionHead>();
            //JobEntryHeads = new HashSet<JobEntryHead>();
            //BranchCultureDatas = new HashSet<BranchCultureData>();
            Employees = new HashSet<Employee>();
            //Saloons = new HashSet<Saloon>();
            //Suppliers = new HashSet<Supplier>();
            EmployeePromotionNewBranches = new HashSet<EmployeePromotion>();
            EmployeePromotionOldBranches = new HashSet<EmployeePromotion>();
            EmployeeESBProvisionHeads = new HashSet<EmployeeESBProvisionHead>();
            EmployeeLSProvisionHeads = new HashSet<EmployeeLSProvisionHead>();
        }

        [Key]
        public long BranchIID { get; set; }

        [StringLength(255)]
        public string BranchName { get; set; }

        public long? BranchGroupID { get; set; }

        public long? WarehouseID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public bool? IsMarketPlace { get; set; }

        public byte? StatusID { get; set; }

        [StringLength(500)]
        public string Logo { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(50)]
        public string Longitude { get; set; }

        [StringLength(50)]
        public string Latitude { get; set; }

        public bool? IsVirtual { get; set; }

        public string BranchCode { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<EmployeeESBProvisionHead> EmployeeESBProvisionHeads { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<EmployeeLSProvisionHead> EmployeeLSProvisionHeads { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Location> Locations { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductInventory> ProductInventories { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TransactionHead> TransactionHeads1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BranchCultureData> BranchCultureDatas { get; set; }

        //public virtual BranchGroup BranchGroup { get; set; }

        //public virtual BranchStatus BranchStatus { get; set; }

        //public virtual Warehouse Warehouse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Saloon> Saloons { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotionNewBranches { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldBranches { get; set; }
    }
}