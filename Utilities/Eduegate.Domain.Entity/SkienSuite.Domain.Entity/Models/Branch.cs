using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Branch
    {
        public Branch()
        {
            this.ProductPriceListBranchMaps = new List<ProductPriceListBranchMap>();
            this.BranchDocumentTypeMaps = new List<BranchDocumentTypeMap>();
            this.InventoryVerifications = new List<InventoryVerification>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.Locations = new List<Location>();
            this.ProductDeliveryTypeMaps = new List<ProductDeliveryTypeMap>();
            this.ProductInventories = new List<ProductInventory>();
            this.ProductInventorySerialMaps = new List<ProductInventorySerialMap>();
            this.TransactionHeads = new List<TransactionHead>();
            this.TransactionHeads1 = new List<TransactionHead>();
            this.JobEntryHeads = new List<JobEntryHead>();
            this.Employees = new List<Employee>();
            this.Suppliers = new List<Supplier>();
        }

        public long BranchIID { get; set; }
        public string BranchName { get; set; }
        public Nullable<long> BranchGroupID { get; set; }
        public Nullable<long> WarehouseID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<bool> IsMarketPlace { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string Logo { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads1 { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual BranchGroup BranchGroup { get; set; }
        public virtual BranchStatus BranchStatus { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
