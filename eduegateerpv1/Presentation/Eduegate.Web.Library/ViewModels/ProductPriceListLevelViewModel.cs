using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductPriceListLevelViewModel
    {
        public short ProductPriceListLevelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static ProductPriceListLevelViewModel ToViewModel(ProductPriceListLevelDTO dto)
        {
            Mapper<ProductPriceListLevelDTO, ProductPriceListLevelViewModel >.CreateMap();
            return Mapper<ProductPriceListLevelDTO, ProductPriceListLevelViewModel>.Map(dto);
        }

        public static ProductPriceListLevelDTO ToDTO(ProductPriceListLevelViewModel vm)
        {
            Mapper<ProductPriceListLevelViewModel, ProductPriceListLevelDTO>.CreateMap();
            return Mapper<ProductPriceListLevelViewModel, ProductPriceListLevelDTO>.Map(vm);
        }
    }
}
