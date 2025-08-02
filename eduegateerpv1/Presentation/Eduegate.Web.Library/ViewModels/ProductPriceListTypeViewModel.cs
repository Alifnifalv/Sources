using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductPriceListTypeViewModel
    {
        public short ProductPriceListTypeID { get; set; }
        public string PriceListTypeName { get; set; }
        public string Description { get; set; }



        public static ProductPriceListTypeViewModel ToViewModel(ProductPriceListTypeDTO dto)
        {
            Mapper<ProductPriceListTypeDTO, ProductPriceListTypeViewModel>.CreateMap();
            return Mapper<ProductPriceListTypeDTO, ProductPriceListTypeViewModel>.Map(dto);
        }

        public static ProductPriceListTypeDTO ToDTO(ProductPriceListTypeViewModel vm)
        {
            Mapper<ProductPriceListTypeViewModel, ProductPriceListTypeDTO>.CreateMap();
            return Mapper<ProductPriceListTypeViewModel, ProductPriceListTypeDTO>.Map(vm);
        }
    }
}
