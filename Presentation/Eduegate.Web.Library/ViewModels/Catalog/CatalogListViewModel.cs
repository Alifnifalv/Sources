using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class CatalogListViewModel : CatalogListBase
    {

        public string BrandNameEn { get; set; }
     
        public string ProductColor { get; set; }
        public long BrandID { get; set; }
        public string BrandCode { get; set; }     
        public string BrandName { get; set; } 
        public short BrandPosition { get; set; }
        public string ProductCategoryAll { get; set; }  
        public long ProductWeight { get; set; }
        public short DeliveryDays { get; set; }
        public int? ProductSoldQty { get; set; }
        public int NewArrival { get; set; }
        public string ProductMadeIn { get; set; }
        public string ProductModel { get; set; }
        public string ProductWarranty { get; set; }
        public bool QuantityDiscount { get; set; }
        public bool DisableListing { get; set; }
        public bool ProductActive { get; set; }
        public bool ProductActiveAr { get; set; }
        public string ProductKeywordsEn { get; set; }
        public string BrandKeywordsEn { get; set; }
        public string ProductNameAr { get; set; }
        public string BrandNameAr { get; set; }
        public int ProductListingQuantity { get; set; }

        public static List<CatalogListViewModel> FromDTO(List<SearchCatalogDTO> dtos)
        {
            var vms = new List<CatalogListViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static CatalogListViewModel FromDTO(SearchCatalogDTO dto)
        {
            Mapper<SearchCatalogDTO, CatalogListViewModel>.CreateMap();
            var mapper = Mapper<SearchCatalogDTO, CatalogListViewModel>.Map(dto);
            if(dto.ProductAvailableQuantity > 0){
                mapper.ProductListingQuantity = 1;
            }
            return mapper;
        }
    }
}
