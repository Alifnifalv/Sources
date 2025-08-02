using System;
using System.Collections.Generic;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Branch : ISecuredEntity
    {
        public Branch()
        {
            this.InventoryVerifications = new List<InventoryVerification>();
            this.InvetoryTransactions = new List<InvetoryTransaction>();
            this.Locations = new List<Location>();
            this.ProductInventories = new List<ProductInventory>();
            this.TransactionHeads = new List<TransactionHead>();
            this.TransactionHeads1 = new List<TransactionHead>();
            this.JobEntryHeads = new List<JobEntryHead>();
            this.Employees = new List<Employee>();
            this.ProductInventorySerialMaps = new List<ProductInventorySerialMap>();
            this.ProductPriceListBranchMaps = new List<ProductPriceListBranchMap>();
            this.BranchDocumentTypeMaps = new List<BranchDocumentTypeMap>();
            this.ProductDeliveryTypeMaps = new List<ProductDeliveryTypeMap>();
            this.BranchCultureDatas = new HashSet<BranchCultureData>();
            this.AccountTransactionHeads = new HashSet<AccountTransactionHead>();
        }

        public long BranchIID { get; set; }
        public string BranchName { get; set; }
        public string Logo { get; set; }
        public Nullable<long> BranchGroupID { get; set; }
        public Nullable<long> WarehouseID { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<bool> IsMarketPlace {get;set;}
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public bool? IsVirtual { get; set; }
        public string BranchCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set;}
        public virtual ICollection<InventoryVerification> InventoryVerifications { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads1 { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual BranchGroup BranchGroup { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual BranchStatus BranchStatuses { get; set; }
        public virtual ICollection<BranchCultureData> BranchCultureDatas { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ProductInventorySerialMap> ProductInventorySerialMaps { get; set; }
        public virtual ICollection<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        public virtual ICollection<BranchDocumentTypeMap> BranchDocumentTypeMaps { get; set; }
        public virtual ICollection<ProductDeliveryTypeMap> ProductDeliveryTypeMaps { get; set; }

        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }

        public long GetIID()
        {
            return this.BranchIID;
        }
    }
}
