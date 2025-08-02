using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
   public class CatalogListBase
    {
        public long ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public string ProductThumbnail { get; set; }
        public string ProductListingImage { get; set; }
        public int ProductAvailableQuantity { get; set; }
        public long SkuID { get; set; }
    }
}
