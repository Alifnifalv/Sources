using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class ProductPriceSettingQuantityViewModel : BaseMasterViewModel
    {
        public long ProductPriceListProductQuantityMapIID { get; set; }
        public Nullable<long> ProductPriceListProductMapID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
    }
}
