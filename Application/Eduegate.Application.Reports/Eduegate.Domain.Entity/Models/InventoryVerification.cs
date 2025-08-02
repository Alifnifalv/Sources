using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class InventoryVerification
    {
        public long InventoryVerificationIID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<System.DateTime> VerificationDate { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> StockQuantity { get; set; }
        public Nullable<decimal> VerifiedQuantity { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
