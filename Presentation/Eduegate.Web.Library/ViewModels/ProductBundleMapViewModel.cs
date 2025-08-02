using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductBundleMapViewModel
    {
        public long BundleID { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public long ProductSKUMapID { get; set; }
        public string ProductSKUName { get; set; }
        public List<ProductItemViewModel> SelectedProducts { get; set; }
        public List<ProductItemViewModel> SelectedProductSKUs { get; set; }
    }
}
