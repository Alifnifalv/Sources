using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.ProductDetail;

namespace Eduegate.Services.Contracts
{
    public class ProductDealDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProductPriceDTO productPriceList { get; set; }

        public List<ProductSKUDetailDTO> products { get; set; }
    }
}
