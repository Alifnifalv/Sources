using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductToProductMapViewModel
    {
        public ProductMapViewModel FromProduct { get; set; }
        public List<ProductMapViewModel> ProductList { get; set; }
        public bool IsFromProductDisabled { get; set; }
        public List<SalesRelationshipTypeViewModel> SalesRelationshipTypes { get; set; }
    }
}
