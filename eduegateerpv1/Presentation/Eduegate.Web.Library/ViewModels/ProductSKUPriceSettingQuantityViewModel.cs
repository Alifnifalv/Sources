using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductSKUPriceSettingQuantityViewModel : BaseMasterViewModel
    {
        public long ProductPriceListSKUQuantityMapIID { get; set; }
        public Nullable<long> ProductPriceListSKUMapID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
    }
}
