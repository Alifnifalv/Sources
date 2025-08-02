using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductPriceBranchViewModel : BaseMasterViewModel
    {
        public ProductPriceBranchViewModel()  
        {
            CustomerGroups = new List<ProductSKUPriceCustomerGroupViewModel>() { };
        }

        public long ProductPriceListBranchMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public string BranchName { get; set; }

        public List<ProductSKUPriceCustomerGroupViewModel> CustomerGroups { get; set; }

        public static ProductPriceBranchViewModel ToVM(BranchMapDTO dto)
        {
            Mapper<BranchMapDTO, ProductPriceBranchViewModel>.CreateMap();
            return Mapper<BranchMapDTO, ProductPriceBranchViewModel>.Map(dto);
        }

        public static BranchMapDTO ToDTO(ProductPriceBranchViewModel vm)
        {
            Mapper<ProductPriceBranchViewModel, BranchMapDTO>.CreateMap();
            return Mapper<ProductPriceBranchViewModel, BranchMapDTO>.Map(vm);
        }
    }
}
