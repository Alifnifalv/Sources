using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductSKUPriceSettingViewModel : BaseMasterViewModel
    {
        public ProductSKUPriceSettingViewModel()
        {
            ProductSKUPriceSettingQuantities = new List<ProductSKUPriceSettingQuantityViewModel>();
        }

        public long ProductPriceListItemMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
        public string PriceListName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public List<ProductSKUPriceSettingQuantityViewModel> ProductSKUPriceSettingQuantities { get; set; }
    }
}
