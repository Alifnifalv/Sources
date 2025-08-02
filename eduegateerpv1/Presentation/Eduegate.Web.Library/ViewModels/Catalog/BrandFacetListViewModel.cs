using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class BrandFacetListViewModel
    {
        public List<BrandFacetItemViewModel> FacetItems { get; set; }

        public static List<BrandFacetListViewModel> FromDTO(List<FacetsDetail> dtos)
        {
            var vms = new List<BrandFacetListViewModel>();
            vms.Add(ToDTO(dtos[1]));
            return vms;
        }

        public static BrandFacetListViewModel ToDTO(FacetsDetail dto)
        {
            return new BrandFacetListViewModel()
            {
                FacetItems = BrandFacetItemViewModel.FromDTO(dto.FaceItems)
            };
        }
    }
}
