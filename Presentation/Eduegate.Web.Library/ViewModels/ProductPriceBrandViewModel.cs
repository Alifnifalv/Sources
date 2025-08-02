using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductPriceBrandViewModel : BaseMasterViewModel
    {
        public ProductPriceBrandViewModel()
        {
            CustomerGroups = new List<ProductSKUPriceCustomerGroupViewModel>() { };
            QuantityPrice = new List<ProductQuantityPriceViewModel>() { new ProductQuantityPriceViewModel() };
        }

        public long ProductPriceListBrandMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public string PriceDescription { get; set; }
        public Nullable<long> BrandID { get; set; }
        public Nullable<decimal> DiscountPrice { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> Price { get; set; }

        public List<ProductSKUPriceCustomerGroupViewModel> CustomerGroups { get; set; }
        public List<ProductQuantityPriceViewModel> QuantityPrice { get; set; }

        public static ProductPriceBrandViewModel FromDTO(ProductPriceBrandDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceBrandViewModel vm = new ProductPriceBrandViewModel();

                vm.ProductPriceListBrandMapIID = dto.ProductPriceListBrandMapIID;
                vm.ProductPriceListID = dto.ProductPriceListID;
                vm.PriceDescription = dto.PriceDescription;
                vm.BrandID = dto.BrandID;
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
                return new ProductPriceBrandViewModel();
            }
        }

        public static ProductPriceBrandDTO ToDTO(ProductPriceBrandViewModel vm)
        {
            if (vm.IsNotNull())
            {
                ProductPriceBrandDTO dto = new ProductPriceBrandDTO();

                dto.ProductPriceListBrandMapIID = vm.ProductPriceListBrandMapIID;
                dto.ProductPriceListID = vm.ProductPriceListID;
                dto.BrandID = vm.BrandID;
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
                return new ProductPriceBrandDTO();
            }
        }
    }
}
