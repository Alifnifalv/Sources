using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
   public class CartProductViewModel
    {
        public long SKU { get; set; }
        public string PriceUnit { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string Details { get; set; }
        public string Size { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvailableQuantity { get; set; }
        public decimal AllowedQuantity { get; set; }
        public long BranchID { get; set; }
        public long CustomerID { get; set; }
    } 
}
