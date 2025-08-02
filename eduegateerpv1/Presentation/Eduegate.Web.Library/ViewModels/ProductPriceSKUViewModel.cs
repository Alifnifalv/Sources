using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductPriceSKUViewModel : BaseMasterViewModel
    {
        public ProductPriceSKUViewModel()
        {
            CustomerGroups = new List<ProductSKUPriceCustomerGroupViewModel>() { new ProductSKUPriceCustomerGroupViewModel() };
            QuantityPrice = new List<ProductQuantityPriceViewModel>();
            BranchMaps = new List<ProductPriceBranchViewModel>();
        }

        public long ProductPriceListItemMapIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> ProductSKUID { get; set; }
        public string ProductName { get; set; }
        public string PartNumber { get; set; }
        public string Barcode { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PricePercentage { get; set; }
        public bool IsDirty { get; set; }
        public string PriceDescription { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public bool IsMarketPlace { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public List<ProductSKUPriceCustomerGroupViewModel> CustomerGroups { get; set; }
        public List<ProductQuantityPriceViewModel> QuantityPrice { get; set; }
        public List<ProductPriceBranchViewModel> BranchMaps { get; set; }
        public KeyValueViewModel PriceListStatus { get; set; }

        public static ProductPriceSKUViewModel FromDTO(ProductPriceSKUDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceSKUViewModel ppsVM = new ProductPriceSKUViewModel();
                ppsVM.QuantityPrice = new List<ProductQuantityPriceViewModel>();
                ProductQuantityPriceViewModel pqpVM = null;

                ppsVM.ProductPriceListItemMapIID = dto.ProductPriceListItemMapIID;
                ppsVM.CompanyID = dto.CompanyID;
                ppsVM.ProductPriceListID = dto.ProductPriceListID;
                ppsVM.ProductSKUID = dto.ProductSKUID;
                ppsVM.Price = dto.Price;
                ppsVM.Discount = dto.Discount;
                ppsVM.DiscountPercentage = dto.DiscountPercentage;
                ppsVM.Cost = dto.Cost;
                ppsVM.PriceDescription = dto.PriceDescription;
                ppsVM.CreatedBy = dto.CreatedBy;
                ppsVM.UpdatedBy = dto.UpdatedBy;
                ppsVM.CreatedDate = dto.CreatedDate;
                ppsVM.UpdatedDate = dto.UpdatedDate;
                ppsVM.TimeStamps = dto.TimeStamps;
                ppsVM.IsMarketPlace = dto.IsMarketPlace;
                ppsVM.PriceListStatus = new KeyValueViewModel();
                ppsVM.PriceListStatus.Key = dto.PriceListStatus.Key;
                ppsVM.PriceListStatus.Value = dto.PriceListStatus.Value;

                if(dto.ProductSKUQuntityLevelPrices.IsNotNull() && dto.ProductSKUQuntityLevelPrices.Count > 0)
                {
                    foreach(var psqlp in dto.ProductSKUQuntityLevelPrices)
                    {
                        pqpVM = new ProductQuantityPriceViewModel();

                        pqpVM.ProductPriceListSKUQuantityMapIID = psqlp.ProductPriceListSKUQuantityMapIID;
                        pqpVM.ProductPriceListSKUMapID = psqlp.ProductPriceListSKUMapID;
                        pqpVM.ProductSKUMapID = psqlp.ProductSKUMapID;
                        pqpVM.Quantity = psqlp.Quantity;
                        pqpVM.Discount = psqlp.Discount;
                        pqpVM.DiscountPercentage = psqlp.DiscountPercentage;
                        pqpVM.CreatedBy = psqlp.CreatedBy;
                        pqpVM.UpdatedBy = psqlp.UpdatedBy;
                        pqpVM.CreatedDate = psqlp.CreatedDate;
                        pqpVM.UpdatedDate = psqlp.UpdatedDate;
                        pqpVM.TimeStamps = psqlp.TimeStamps;

                        ppsVM.QuantityPrice.Add(pqpVM);

                    }
                }

                return ppsVM;
            }
            else
            {
                return new ProductPriceSKUViewModel();
            }
        }

        public static ProductPriceSKUDTO ToDTO(ProductPriceSKUViewModel vm)
        {
            if (vm.IsNotNull())
            {
                ProductPriceSKUDTO ppsDTO = new ProductPriceSKUDTO();
                ppsDTO.ProductSKUQuntityLevelPrices = new List<QuantityPriceDTO>();
                QuantityPriceDTO qpDTO = null;

                ppsDTO.ProductPriceListItemMapIID = vm.ProductPriceListItemMapIID;
                ppsDTO.CompanyID = vm.CompanyID;
                ppsDTO.ProductPriceListID = vm.ProductPriceListID;
                ppsDTO.ProductSKUID = vm.ProductSKUID;
                ppsDTO.Price = vm.Price;
                ppsDTO.Discount = vm.Discount;
                ppsDTO.DiscountPercentage = vm.DiscountPercentage;
                ppsDTO.Cost = vm.Cost;
                ppsDTO.PriceDescription = vm.PriceDescription;
                ppsDTO.IsMarketPlace = vm.IsMarketPlace;
                ppsDTO.CreatedBy = vm.CreatedBy;
                ppsDTO.UpdatedBy = vm.UpdatedBy;
                ppsDTO.CreatedDate = vm.CreatedDate;
                ppsDTO.UpdatedDate = vm.UpdatedDate;
                ppsDTO.TimeStamps = vm.TimeStamps;
                ppsDTO.PriceListStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO();
                ppsDTO.PriceListStatus.Key = vm.PriceListStatus.IsNull()  ? "1" : vm.PriceListStatus.Key;

                if (vm.QuantityPrice.IsNotNull() && vm.QuantityPrice.Count > 0)
                {
                    foreach(var qp in vm.QuantityPrice)
                    {
                        qpDTO = new QuantityPriceDTO();

                        qpDTO.ProductPriceListSKUQuantityMapIID = qp.ProductPriceListSKUQuantityMapIID;
                        qpDTO.ProductPriceListSKUMapID = qp.ProductPriceListSKUMapID.IsNotNull() ? qp.ProductPriceListSKUMapID : vm.ProductPriceListItemMapIID;
                        qpDTO.ProductSKUMapID = qp.ProductSKUMapID.IsNotNull() ? qp.ProductSKUMapID : vm.ProductSKUID;
                        qpDTO.Quantity = qp.Quantity;
                        qpDTO.Discount = qp.Discount;
                        qpDTO.DiscountPercentage = qp.DiscountPercentage;
                        qpDTO.CreatedBy = qp.CreatedBy;
                        qpDTO.UpdatedBy = qp.UpdatedBy;
                        qpDTO.CreatedDate = qp.CreatedDate;
                        qpDTO.UpdatedDate = qp.UpdatedDate;
                        qpDTO.TimeStamps = qp.TimeStamps;

                        ppsDTO.ProductSKUQuntityLevelPrices.Add(qpDTO);
                    }
                }

                return ppsDTO;
            }
            else
            {
                return new ProductPriceSKUDTO();
            }
        }
    }
}