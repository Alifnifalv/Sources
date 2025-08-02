using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Eduegate.Domain;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductPriceViewModel : BaseMasterViewModel
    {
        private static string dateTimeFormat { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat"); } }

        public ProductPriceViewModel()
        {
            SKUPrice = new List<ProductPriceSKUViewModel>() { new ProductPriceSKUViewModel() };
            CategoryPrice = new List<ProductPriceCategoryViewModel>() { new ProductPriceCategoryViewModel() };
            BrandPrice = new List<ProductPriceBrandViewModel>() { new ProductPriceBrandViewModel() };
            BranchMaps = new List<ProductPriceBranchViewModel>();
        }

        public long ProductPriceListIID { get; set; }
        public string PriceDescription { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public Nullable<short> ProductPriceListTypeID { get; set; }
        public Nullable<short> ProductPriceListLevelID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<ProductPriceSKUViewModel> SKUPrice { get; set; }
        public List<ProductPriceCategoryViewModel> CategoryPrice { get; set; }
        public List<ProductPriceBrandViewModel> BrandPrice { get; set; }
        public List<ProductPriceBranchViewModel> BranchMaps { get; set; }

        public static ProductPriceViewModel FromDTO(ProductPriceDTO dto)
        {
            ProductPriceViewModel priceViewModel = new ProductPriceViewModel();
            priceViewModel.ProductPriceListIID = dto.ProductPriceListIID;
            priceViewModel.PriceDescription = dto.PriceDescription;
            priceViewModel.Price = dto.Price;
            priceViewModel.Quantity = dto.Quantity;
            priceViewModel.PricePercentage = dto.PricePercentage;
            priceViewModel.ProductPriceListTypeID = dto.ProductPriceListTypeID;
            priceViewModel.ProductPriceListLevelID = dto.ProductPriceListLevelID;
            priceViewModel.StartDate = dto.StartDate.IsNull() ? null : Convert.ToDateTime(dto.StartDate).ToString(dateTimeFormat); // DateTime.ParseExact; dateFormat; CultureInfo.InvariantCulture).ToString();
            priceViewModel.EndDate = dto.EndDate.IsNull() ? null : Convert.ToDateTime(dto.EndDate).ToString(dateTimeFormat);//DateTime.ParseExact(Convert.ToDateTime(dto.EndDate).ToShortDateString(); dateFormat; CultureInfo.InvariantCulture).ToString();
            priceViewModel.CreatedDate = dto.CreatedDate;
            priceViewModel.UpdatedDate = dto.UpdatedDate;
            priceViewModel.CreatedBy = dto.CreatedBy;
            priceViewModel.UpdatedBy = dto.UpdatedBy;
            priceViewModel.TimeStamps = dto.TimeStamps;

            if (dto.BranchMaps.IsNotNull())
            {
                foreach (BranchMapDTO branchMapModel in dto.BranchMaps)
                {
                    priceViewModel.BranchMaps.Add(ProductPriceBranchViewModel.ToVM(branchMapModel));
                }
            }
            else
            {
                priceViewModel.BranchMaps.Add(new ProductPriceBranchViewModel());
            }

            return priceViewModel;
        }

        public static ProductPriceDTO ToDTO(ProductPriceViewModel vm)
        {
            ProductPriceDTO dto = new ProductPriceDTO();
            dto.ProductPriceListIID = vm.ProductPriceListIID;
            dto.PriceDescription = vm.PriceDescription;
            dto.Quantity = vm.Quantity;
            dto.Price = vm.Price;
            dto.PricePercentage = vm.PricePercentage;
            dto.ProductPriceListTypeID = vm.ProductPriceListTypeID;
            dto.ProductPriceListLevelID = vm.ProductPriceListLevelID;
            dto.StartDate = vm.StartDate.IsNull() ? dto.StartDate : DateTime.ParseExact(vm.StartDate, dateTimeFormat, CultureInfo.InvariantCulture);
            dto.EndDate = vm.EndDate.IsNull() ? dto.EndDate : DateTime.ParseExact(vm.EndDate, dateTimeFormat, CultureInfo.InvariantCulture);
            dto.CreatedDate = vm.CreatedDate;
            dto.CreatedBy = vm.CreatedBy;
            dto.UpdatedDate = vm.UpdatedDate;
            dto.UpdatedBy = vm.UpdatedBy;
            dto.TimeStamps = vm.TimeStamps;
            dto.BranchMaps = new List<BranchMapDTO>();

            if (vm.BranchMaps.IsNotNull())
            {
                foreach (ProductPriceBranchViewModel bvm in vm.BranchMaps)
                {
                    if (bvm.BranchID != null && bvm.BranchID != 0)
                    {
                        dto.BranchMaps.Add(new BranchMapDTO()
                        {
                            BranchID = bvm.BranchID,
                            ProductPriceListBranchMapIID = bvm.ProductPriceListBranchMapIID,
                            ProductPriceListID = bvm.ProductPriceListID,
                            CreatedBy = bvm.CreatedBy,
                            UpdatedBy = bvm.UpdatedBy,
                            CreatedDate = bvm.CreatedDate,
                            UpdatedDate = bvm.UpdatedDate,
                            TimeStamps = bvm.TimeStamps,
                        });
                    }
                }
            }
            return dto;
        }
    }


}