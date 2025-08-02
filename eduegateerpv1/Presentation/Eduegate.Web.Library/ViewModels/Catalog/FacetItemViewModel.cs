using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class FacetItemViewModel
    {
        public FacetItemViewModel()
        {
            ChildFacetItems = new List<FacetItemViewModel>();
        }

        public string Key { get; set; }
        public int Value { get; set; }
        public string Code { get; set; }
        public bool IsSelected { get; set; }
        public bool IsMainCategory { get; set; }

        public List<FacetItemViewModel> ChildFacetItems { get; set; }

        public static List<FacetItemViewModel> FromDTO(List<FacetItem> dtos)
        {
            var vms = new List<FacetItemViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(ToDTO(dto));
            }

            return vms;
        }

        public static FacetItemViewModel ToDTO(FacetItem dto)
        {
            Mapper<FacetItem, FacetItemViewModel>.CreateMap();
            return Mapper<FacetItem, FacetItemViewModel>.Map(dto);
        }
    }
}
