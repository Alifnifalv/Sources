using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class CategoryFacetListViewModel
    {
        public CategoryFacetListViewModel()
        {
            FacetItems = new List<CategoryFacetItemViewModel>();
            FacetSelectedItems = new List<CategoryFacetItemViewModel>();
        }

        public List<CategoryFacetItemViewModel> FacetItems { get; set; }
        public List<CategoryFacetItemViewModel> FacetSelectedItems { get; set; }

        public static List<CategoryFacetListViewModel> FromDTO(List<FacetsDetail> dtos)
        {
            var vms = new List<CategoryFacetListViewModel>();
            vms.Add(ToDTO(dtos[0]));
            return vms;
        }

        public static CategoryFacetListViewModel ToDTO(FacetsDetail dto)
        {
            return new CategoryFacetListViewModel()
            {
                FacetItems = CategoryFacetItemViewModel.FromDTO(dto.FaceItems)
            };
        }
    }
}
