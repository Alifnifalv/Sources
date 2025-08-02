using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
  public class SaveForLaterViewModel
    {
        public long ProductIID { get; set; }

        public string ProductName { get; set; }

        public Nullable<long> SKUID { get; set; }

        public string ProductCategory { get; set; }

        public string ProductSubCategory { get; set; }

        public decimal DesignerIID { get; set; }

        public string DesignerName { get; set; }

        public string ProductPrice { get; set; }

        public string ProductImageUrl { get; set; }

        public Nullable<decimal> SellingQuantityLimit { get; set; }

        public Nullable<decimal> Quantity { get; set; }

        public string ProductDisCountedPrice { get; set; }

        public string Currency { get; set; }
    }
}
