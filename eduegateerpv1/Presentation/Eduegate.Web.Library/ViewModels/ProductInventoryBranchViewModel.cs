using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductInventoryBranchViewModel : BaseMasterViewModel
    {

        public long ProductSKUMapID { get; set; }

        public long BranchID { get; set; }


        public Decimal Quantity { get; set; }


        public bool IsMarketPlace { get; set; }


        public decimal ProductCostPrice { get; set; }


        public decimal ProductPricePrice { get; set; }

        public decimal ProductDiscountPrice { get; set; }

        public static ProductInventoryBranchViewModel ToVM(ProductInventoryBranchDTO dto)
        {
            Mapper<ProductInventoryBranchDTO, ProductInventoryBranchViewModel>.CreateMap();
            var mapper = Mapper<ProductInventoryBranchDTO, ProductInventoryBranchViewModel>.Map(dto);

            return mapper;
        }


    }
}
