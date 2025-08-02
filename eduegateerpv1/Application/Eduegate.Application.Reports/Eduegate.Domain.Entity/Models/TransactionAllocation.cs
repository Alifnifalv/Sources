using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionAllocation
    {
        public long TransactionAllocationIID { get; set; }
        public Nullable<long> TrasactionDetailID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        //public virtual TransactionAllocation TransactionAllocations1 { get; set; }
        //public virtual TransactionAllocation TransactionAllocation1 { get; set; }
        public virtual TransactionDetail TransactionDetail { get; set; }
    }
}
