using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class BrandFacetItemViewModel
    {
        public string Key { get; set; }

        public int Value { get; set; }

        public string Code { get; set; }
        public bool Selected { get; set; }

        //private bool selected = true;
        //public bool Selected
        //{
        //    get
        //    {
        //        return selected;
        //    }
        //    set
        //    {
        //        selected = value;
        //    }
        //}
        public static List<BrandFacetItemViewModel> FromDTO(List<FacetItem> dtos)
        {
            var vms = new List<BrandFacetItemViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(ToDTO(dto));
            }

            return vms;
        }

        public static BrandFacetItemViewModel ToDTO(FacetItem dto)
        {
            Mapper<FacetItem, BrandFacetItemViewModel>.CreateMap();
            return Mapper<FacetItem, BrandFacetItemViewModel>.Map(dto);
        }
    }
}
