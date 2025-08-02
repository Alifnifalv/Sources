using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library
{
   public class ProductBundlesViewModel
    {
       public long BundleProductID { get; set; }
       public List<ProductBundleMapViewModel> ProductMaps { get; set; }
       public List<ProductBundleMapViewModel> SKUMaps { get; set; }
    }
}
