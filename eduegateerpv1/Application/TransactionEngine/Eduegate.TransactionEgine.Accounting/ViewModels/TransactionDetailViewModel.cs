using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.TransactionEgine.Accounting.ViewModels
{
    public class TransactionDetailViewModel
    {
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

        public Nullable<long> CreatedBy { get; set; }

        public Nullable<long> UpdatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }
        public long? BatchID { get; set; }

        public decimal ForeignAmount { get; set; }
        public decimal ForeignRate { get; set; }      
      
        public TransactionHeadViewModel TransactionHead { get; set; }
        public List<TransactionAllocationViewModel> TransactionAllocations { get; set; }
    }
}
