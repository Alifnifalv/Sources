using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class FacetListViewModel
    {
        public string FacetCode { get; set; }
        public string FacetName { get; set; }
        public List<FacetItemViewModel> FacetItems { get; set; }
        public List<FacetItemViewModel> FacetSelectedItems { get; set; }
        public bool IsMultiSelection { get; set; }

        public FacetListViewModel()
        {
            FacetItems = new List<FacetItemViewModel>();
            FacetSelectedItems = new List<FacetItemViewModel>();
            IsMultiSelection = true;
        }

        public static List<FacetListViewModel> FromDTO(List<FacetsDetail> dtos)
        {
            var categoryFacets = dtos.FirstOrDefault(a=> a.Name == "cat");
            var brandFacets = dtos.FirstOrDefault(a=> a.Name == "brandname_code");

            var vms = new List<FacetListViewModel>() { 
                 categoryFacets != null ? ToDTO("cat", Eduegate.Globalization.Resources.Categories, categoryFacets) : new FacetListViewModel()
                , brandFacets != null ? ToDTO("brandcode", Eduegate.Globalization.Resources.Brands, brandFacets) : new FacetListViewModel() 
            };
            return vms;
        }

        public static FacetListViewModel ToDTO(string facetCode, string facetName, FacetsDetail dto)
        {
            if (dto == null)
                return null;

            return new FacetListViewModel()
            {
                FacetCode = facetCode,
                FacetName = facetName,
                FacetItems = FacetItemViewModel.FromDTO(dto.FaceItems.OrderBy(a=> a.key).ToList()),
                IsMultiSelection = facetCode == "cat" ? false : true,
            };
        }
    }
}
