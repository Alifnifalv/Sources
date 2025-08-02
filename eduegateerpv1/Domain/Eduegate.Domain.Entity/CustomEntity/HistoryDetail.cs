using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.CustomEntity
{
    public class HistoryDetail
    {
        public long TransactionIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string ProductName { get; set; }
        public string SerialNumber { get; set; }
        public string ProductImageUrl { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public string PartNumber { get; set; }
        public long DetailIID { get; set; }
        public Nullable<decimal> CancelQuantity { get; set; }
        public Nullable<decimal> ActualQuantity { get; set; }
        public string BarCode { get; set; }

    }
}
