using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEngineCore.ViewModels
{
    public class TransactionAllocationViewModel
    {
       
        public long TransactionAllocationIID { get; set; }
       
        public Nullable<long> TrasactionDetailID { get; set; }
       
        public Nullable<long> BranchID { get; set; }
       
        public Nullable<decimal> Quantity { get; set; }
       
        public Nullable<int> CreatedBy { get; set; }
       
        public Nullable<int> UpdatedBy { get; set; }
       
        public Nullable<System.DateTime> CreatedDate { get; set; }
       
        public Nullable<System.DateTime> UpdatedDate { get; set; }
       
        public string TimeStamps { get; set; }
    }
}
