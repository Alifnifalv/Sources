using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductPriceCategoryViewModel : BaseMasterViewModel
    {
        public ProductPriceCategoryViewModel()
        {
            CustomerGroups = new List<ProductSKUPriceCustomerGroupViewModel>() { };
            QuantityPrice = new List<ProductQuantityPriceViewModel>() { new ProductQuantityPriceViewModel() };
        }

        public long ProductPriceListCategoryMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public string PriceDescription { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> Price { get; set; }

        public List<ProductSKUPriceCustomerGroupViewModel> CustomerGroups { get; set; }

        public List<ProductQuantityPriceViewModel> QuantityPrice { get; set; }

        public static ProductPriceCategoryViewModel FromDTO(ProductPriceCategoryDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceCategoryViewModel vm = new ProductPriceCategoryViewModel();

                vm.ProductPriceListCategoryMapIID = dto.ProductPriceListCategoryMapIID;
                vm.ProductPriceListID = dto.ProductPriceListID;
                vm.PriceDescription = dto.PriceDescription; 
                vm.CategoryID = dto.CategoryID;
                vm.Price = dto.Price;
                vm.DiscountPrice = dto.DiscountPrice;
                vm.DiscountPercentage = dto.DiscountPercentage;
                vm.CreatedBy = dto.CreatedBy;
                vm.CreatedDate = dto.CreatedDate;
                vm.UpdatedBy = dto.UpdatedBy;
                vm.UpdatedDate = dto.UpdatedDate;
                vm.TimeStamps = dto.TimeStamps;

                return vm;
            }
            else
            {
                return new ProductPriceCategoryViewModel();
            }
        }

        public static ProductPriceCategoryDTO ToDTO(ProductPriceCategoryViewModel vm)
        {
            if (vm.IsNotNull())
            {
                ProductPriceCategoryDTO dto = new ProductPriceCategoryDTO();

                dto.ProductPriceListCategoryMapIID = vm.ProductPriceListCategoryMapIID;
                dto.ProductPriceListID = vm.ProductPriceListID;
                dto.CategoryID = vm.CategoryID;
                dto.PriceDescription = vm.PriceDescription;
                dto.Price = vm.Price;
                dto.DiscountPrice = vm.DiscountPrice;
                dto.DiscountPercentage = vm.DiscountPercentage;
                dto.CreatedBy = vm.CreatedBy;
                dto.CreatedDate = vm.CreatedDate;
                dto.UpdatedBy = vm.UpdatedBy;
                dto.UpdatedDate = vm.UpdatedDate;
                dto.TimeStamps = vm.TimeStamps;

                return dto;
            }
            else
            {
                return new ProductPriceCategoryDTO();
            }
        }
    }
}
