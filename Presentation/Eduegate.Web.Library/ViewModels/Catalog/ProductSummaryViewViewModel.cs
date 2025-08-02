using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class ProductSummaryViewViewModel
    {
        public ProductSummaryViewViewModel()
        {

        }
        public AddProductDTO ProductDetails { get; set; }
    }
}
