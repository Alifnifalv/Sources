using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Branches", Schema = "mutual")]
    public partial class Branch
    {
        public Branch()
        {
            AccountTransactionHeads = new HashSet<AccountTransactionHead>();
            AssetInventories = new HashSet<AssetInventory>();
            AssetInventoryTransactions = new HashSet<AssetInventoryTransaction>();
            BranchCultureDatas = new HashSet<BranchCultureData>();
            BranchDocumentTypeMaps = new HashSet<BranchDocumentTypeMap>();
            DeliveryTimeSlotBranchMaps = new HashSet<DeliveryTimeSlotBranchMap>();
            DeliveryTypeGeoMaps = new HashSet<DeliveryTypeGeoMap>();
            EmployeeESBProvisionHeads = new HashSet<EmployeeESBProvisionHead>();
            EmployeeLSProvisionHeads = new HashSet<EmployeeLSProvisionHead>();
            EmployeePromotionNewBranches = new HashSet<EmployeePromotion>();
            EmployeePromotionOldBranches = new HashSet<EmployeePromotion>();
            Employees = new HashSet<Employee>();
            InventoryVerifications = new HashSet<InventoryVerification>();
            InvetoryTransactions = new HashSet<InvetoryTransaction>();
            JobEntryHeads = new HashSet<JobEntryHead>();
            Locations = new HashSet<Location>();
            ProductDeliveryTypeMaps = new HashSet<ProductDeliveryTypeMap>();
            ProductInventories = new HashSet<ProductInventory>();
            ProductInventorySerialMaps = new HashSet<ProductInventorySerialMap>();
            ProductPriceListBranchMaps = new HashSet<ProductPriceListBranchMap>();
            ProductSKUBranchMaps = new HashSet<ProductSKUBranchMap>();
            Saloons = new HashSet<Saloon>();
            Suppliers = new HashSet<Supplier>();
            TransactionHeadBranches = new HashSet<TransactionHead>();
            TransactionHeadToBranches = new HashSet<TransactionHead>();
        }

        [Key]
        public long BranchIID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        public long? BranchGroupID { get; set; }
        public long? WarehouseID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsMarketPlace { get; set; }
        public byte? StatusID { get; set; }
        [StringLength(500)]
        public string Logo { get; set; }
        public int? CompanyID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Longitude { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Latitude { get; set; }
        public bool? IsVirtual { get; set; }
        [StringLength(50)]
        public string BranchCode { get; set; }
        [StringLength(50)]
        public string TransactionNoPrefix { get; set; }
        public int? SortOrder { get; set; }

        [ForeignKey("BranchGroupID")]
        [InverseProperty("Branches")]
        public virtual BranchGroup BranchGroup { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("Branches")]
        public virtual BranchStatus Status { get; set; }
        [ForeignKey("WarehouseID")]
        [InverseProperty("Branches")]
        public virtual Warehous Warehouse { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<AssetInventory> AssetInventories { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<AssetInventoryTransaction> AssetInventoryTransactions { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<BranchCultureData> BranchCultureDatas { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<DeliveryTimeSlotBranchMap> DeliveryTimeSlotBranchMaps { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<DeliveryTypeGeoMap> DeliveryTypeGeoMaps { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<EmployeeESBProvisionHead> EmployeeESBProvisionHeads { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<EmployeeLSProvisionHead> EmployeeLSProvisionHeads { get; set; }
        [InverseProperty("NewBranch")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionNewBranches { get; set; }
        [InverseProperty("OldBranch")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldBranches { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<Location> Locations { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<ProductSKUBranchMap> ProductSKUBranchMaps { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<Saloon> Saloons { get; set; }
        [InverseProperty("BlockedBranch")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [InverseProperty("Branch")]
        public virtual ICollection<TransactionHead> TransactionHeadBranches { get; set; }
        [InverseProperty("ToBranch")]
        public virtual ICollection<TransactionHead> TransactionHeadToBranches { get; set; }
    }
}
