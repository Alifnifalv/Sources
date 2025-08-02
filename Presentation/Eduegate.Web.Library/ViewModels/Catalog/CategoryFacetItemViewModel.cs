using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class CategoryFacetItemViewModel
    {
        public string Key { get; set; }

        public int Value { get; set; }

        public string Code { get; set; }

        public static List<CategoryFacetItemViewModel> FromDTO(List<FacetItem> dtos)
        {
            var vms = new List<CategoryFacetItemViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(ToDTO(dto));
            }

            return vms;
        }

        public static CategoryFacetItemViewModel ToDTO(FacetItem dto)
        {
            Mapper<FacetItem, CategoryFacetItemViewModel>.CreateMap();
            return Mapper<FacetItem, CategoryFacetItemViewModel>.Map(dto);
        }
    }
}
