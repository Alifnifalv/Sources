using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductInventory
    {
        public long ProductSKUMapID { get; set; }
        public long Batch { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> isActive { get; set; }        
        public Nullable<long> BranchID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> OriginalQty { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<Boolean> IsMarketPlaceBranch { get; set; }
        public long? HeadID { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
    }
}
