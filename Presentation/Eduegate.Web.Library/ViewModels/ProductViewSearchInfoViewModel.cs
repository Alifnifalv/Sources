using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductViewSearchInfoViewModel
    {
        public string SortByOption { get; set; }
        public PaginationViewModel PageDetails { get; set; }

        public static ProductViewSearchInfoDTO ToDTO(ProductViewSearchInfoViewModel vm)
        {
            Mapper<ProductViewSearchInfoViewModel, ProductViewSearchInfoDTO>.CreateMap();
            return Mapper<ProductViewSearchInfoViewModel, ProductViewSearchInfoDTO>.Map(vm);
        }
    }
}