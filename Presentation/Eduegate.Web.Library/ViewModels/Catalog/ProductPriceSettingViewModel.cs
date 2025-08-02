using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.PriceSettings;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class ProductPriceSettingViewModel : BaseMasterViewModel
    {
        public ProductPriceSettingViewModel()
        {
            QuantityPrice = new List<ProductPriceSettingQuantityViewModel>();
        }

        public long ProductPriceListProductMapIID { get; set; }

        public Nullable<long> ProductPriceListID { get; set; }

        public Nullable<long> ProductID { get; set; }

        public string PriceDescription { get; set; }

        public Nullable<decimal> Price { get; set; }

        public Nullable<decimal> Discount { get; set; }

        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<int> CompanyID { get; set; }


        public bool ApplyToAllSKUs { get; set; }

        public List<ProductPriceSettingQuantityViewModel> QuantityPrice { get; set; }


        public static ProductPriceSettingDTO ToDTO(ProductPriceSettingViewModel vm)
        {
            if (vm.IsNotNull())
            {
                ProductPriceSettingDTO dto = new ProductPriceSettingDTO();
                dto.ProductQuntityLevelPrices = new List<ProductPriceSettingQuantityDTO>();
                ProductPriceSettingQuantityDTO quantityDTO = null;

                dto.ProductPriceListProductMapIID = vm.ProductPriceListProductMapIID;
                dto.CompanyID = vm.CompanyID;                               
                dto.ProductPriceListID = vm.ProductPriceListID;
                dto.ProductID = vm.ProductID;
                dto.PriceDescription = vm.PriceDescription;
                dto.Price = vm.Price;
                dto.Discount = vm.Discount;
                dto.DiscountPercentage = vm.DiscountPercentage;
                dto.CreatedBy = vm.CreatedBy;
                dto.CreatedDate = vm.CreatedDate;
                dto.UpdatedBy = vm.UpdatedBy;
                dto.UpdatedDate = vm.UpdatedDate;
                dto.TimeStamps = vm.TimeStamps;

                if(vm.QuantityPrice.IsNotNull() && vm.QuantityPrice.Count > 0)
                {
                    foreach(var quantityVM in vm.QuantityPrice)
                    {
                        quantityDTO = new ProductPriceSettingQuantityDTO();

                        quantityDTO.ProductPriceListProductQuantityMapIID = quantityVM.ProductPriceListProductQuantityMapIID;
                        quantityDTO.ProductPriceListProductMapID = quantityVM.ProductPriceListProductMapID.IsNotNull() ? quantityVM.ProductPriceListProductMapID : vm.ProductPriceListProductMapIID;
                        quantityDTO.ProductID = quantityVM.ProductID.IsNotNull() ? quantityVM.ProductID : vm.ProductID;
                        quantityDTO.Quantity = quantityVM.Quantity;
                        quantityDTO.Discount = quantityVM.Discount;
                        quantityDTO.DiscountPercentage = quantityVM.DiscountPercentage;
                        quantityDTO.CreatedBy = quantityVM.CreatedBy;
                        quantityDTO.CreatedDate = quantityVM.CreatedDate;
                        quantityDTO.UpdatedBy = quantityVM.UpdatedBy;
                        quantityDTO.UpdatedDate = quantityVM.UpdatedDate;
                        quantityDTO.TimeStamps = quantityVM.TimeStamps;

                        dto.ProductQuntityLevelPrices.Add(quantityDTO);
                    }
                }

                return dto;
            }
            else
            {
                return new ProductPriceSettingDTO();
            }
        }

        public static ProductPriceSettingViewModel ToViewModel(ProductPriceSettingDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceSettingViewModel vm = new ProductPriceSettingViewModel();
                vm.QuantityPrice = new List<ProductPriceSettingQuantityViewModel>();
                ProductPriceSettingQuantityViewModel quantityVM = null;

                vm.ProductPriceListProductMapIID = dto.ProductPriceListProductMapIID;
                vm.CompanyID = dto.CompanyID;
                vm.ProductPriceListID = dto.ProductPriceListID;
                vm.ProductID = dto.ProductID;
                vm.PriceDescription = dto.PriceDescription;
                vm.Price = dto.Price;
                vm.Discount = dto.Discount;
                vm.DiscountPercentage = dto.DiscountPercentage;
                vm.CreatedBy = dto.CreatedBy;
                vm.CreatedDate = dto.CreatedDate;
                vm.UpdatedBy = dto.UpdatedBy;
                vm.UpdatedDate = dto.UpdatedDate;
                vm.TimeStamps = dto.TimeStamps;

                if (dto.ProductQuntityLevelPrices.IsNotNull() && dto.ProductQuntityLevelPrices.Count > 0)
                {
                    foreach (var quantityDTO in dto.ProductQuntityLevelPrices)
                    {
                        quantityVM = new ProductPriceSettingQuantityViewModel();

                        quantityVM.ProductPriceListProductQuantityMapIID = quantityDTO.ProductPriceListProductQuantityMapIID;
                        quantityVM.ProductPriceListProductMapID = quantityDTO.ProductPriceListProductMapID;
                        quantityVM.ProductID = quantityDTO.ProductID;
                        quantityVM.Quantity = quantityDTO.Quantity;
                        quantityVM.Discount = quantityDTO.Discount;
                        quantityVM.DiscountPercentage = quantityDTO.DiscountPercentage;
                        quantityVM.CreatedBy = quantityDTO.CreatedBy;
                        quantityVM.CreatedDate = quantityDTO.CreatedDate;
                        quantityVM.UpdatedBy = quantityDTO.UpdatedBy;
                        quantityVM.UpdatedDate = quantityDTO.UpdatedDate;
                        quantityVM.TimeStamps = quantityDTO.TimeStamps;

                        vm.QuantityPrice.Add(quantityVM);
                    }
                }

                return vm;
            }
            else
            {
                return new ProductPriceSettingViewModel();
            }
        }

    }
}
