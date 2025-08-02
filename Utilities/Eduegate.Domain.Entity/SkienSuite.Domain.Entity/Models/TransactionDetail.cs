using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionDetail
    {
        public TransactionDetail()
        {
            this.ProductSerialMaps = new List<ProductSerialMap>();
            this.TransactionAllocations = new List<TransactionAllocation>();
            this.TransactionDetails1 = new List<TransactionDetail>();
        }

        public long DetailIID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.DateTime> WarrantyDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<long> ParentDetailID { get; set; }
        public Nullable<int> Action { get; set; }
        public string Remark { get; set; }
        public virtual ICollection<ProductSerialMap> ProductSerialMaps { get; set; }
        public virtual ICollection<TransactionAllocation> TransactionAllocations { get; set; }
        public virtual ICollection<TransactionDetail> TransactionDetails1 { get; set; }
        public virtual TransactionDetail TransactionDetail1 { get; set; }
    }
}
