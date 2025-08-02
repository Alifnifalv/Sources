using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class SKUViewModel : BaseMasterViewModel
    {
        public SKUViewModel()
        {
            ProductInventoryConfigViewModels = new ProductInventoryConfigViewModel();
            ProductInventorySKUConfigMaps = new List<ProductInventorySKUConfigMapViewModel>();
        }

        public long ProductID { get; set; }

        public string ProductSKUCode { get; set; }

        public int Sequence { get; set; }

        public decimal ProductPrice { get; set; }

        public string PartNumber { get; set; }

        public string BarCode { get; set; }

        public string SKU { get; set; }
        public MultiLanguageText SkuName { get; set; }

        public Nullable<byte> StatusID { get; set; }

        public long ProductSKUMapID { get; set; }

        public List<SKUImageMapViewModel> ImageMaps { get; set; }

        public List<SKUVideoMapViewModel> VideoMaps { get; set; }

        public bool isDefaultSKU { get; set; }

        public List<PropertyViewModel> Properties { get; set; }

        public bool? IsHiddenFromList { get; set; }

        public bool HideSKU { get; set; }

        public string VariantsMap { get; set; }

        public Nullable<decimal> CostPrice { get; set; }
        public Nullable<decimal> SellingPrice { get; set; }
        public Nullable<int> Quantity { get; set; }

        public Nullable<long> SeoMetadataID { get; set; }

        public ProductInventoryConfigViewModel ProductInventoryConfigViewModels { get; set; }
        public List<CultureDataInfoViewModel> SupportedCultures { get; set; }
        public List<ProductSKUCultureDataDTO> SKUClutureDTO { get; set; }
        public List<ProductInventorySKUConfigMapViewModel> ProductInventorySKUConfigMaps { get; set; }

        // Mappers
        public static SKUDTO ToDTO(SKUViewModel vm)
        {
            Mapper<SKUViewModel, SKUDTO>.CreateMap();
            Mapper<ProductInventorySKUConfigMapViewModel, ProductInventorySKUConfigMapDTO>.CreateMap();
            Mapper<ProductInventoryConfigViewModel, ProductInventoryConfigDTO>.CreateMap();
            var mapper = Mapper<SKUViewModel, SKUDTO>.Map(vm);
            mapper.ProductInventoryConfigDTOs = Mapper<ProductInventoryConfigViewModel, ProductInventoryConfigDTO>.Map(vm.ProductInventoryConfigViewModels);
            //mapper.ProductInventoryConfigDTOs = Mapper<ProductInventorySKUConfigMapViewModel, ProductInventorySKUConfigMapDTO>.Map(vm.ProductInventoryConfigViewModels.pr);
            return mapper;
        }

        public void InitializeCultureData(List<CultureDataInfoViewModel> datas)
        {
            SupportedCultures = datas;
            SkuName = new MultiLanguageText(datas);

        }

        public static SKUViewModel ToVM(SKUDTO dto)
        {
            Mapper<SKUDTO, SKUViewModel>.CreateMap();
            Mapper<ProductInventorySKUConfigMapDTO, ProductInventorySKUConfigMapViewModel>.CreateMap();
            Mapper<ProductInventoryConfigDTO, ProductInventoryConfigViewModel>.CreateMap();
            return Mapper<SKUDTO, SKUViewModel>.Map(dto);
        }

        public static SKUViewModel FromDTO(SKUDTO dto, List<CultureDataInfoDTO> cultures)
        {
            if (dto == null)
                return null;
            var cultureVm = CultureDataInfoViewModel.FromDTO(cultures);
            var skuviewmodel = new SKUViewModel() { SkuName = new MultiLanguageText() { Text = dto.SkuName, CultureDatas = cultureVm } };
            skuviewmodel.BarCode = dto.BarCode;
            skuviewmodel.PartNumber = dto.PartNumber;
            skuviewmodel.ProductID = dto.ProductID;
            skuviewmodel.ProductPrice = dto.ProductPrice != null ? Convert.ToDecimal(dto.ProductPrice) : 0;
            skuviewmodel.ProductSKUCode = dto.ProductSKUCode;
            skuviewmodel.Sequence = dto.Sequence != null ? Convert.ToInt32(dto.Sequence) : 0;
            skuviewmodel.SKU = dto.SKU;
            // skuviewmodel.SkuName.Text = dto.SkuName;
            skuviewmodel.ProductSKUMapID = dto.ProductSKUMapID;
            skuviewmodel.StatusID = dto.StatusID;
            skuviewmodel.ImageMaps = new List<SKUImageMapViewModel>();

            if (cultures != null)
            {
                bool isFirst = true;

                foreach (var culture in cultureVm)
                {
                    var cultureDTO = dto.ProductSKUCultureInfo.FirstOrDefault(a => a.CultureID == culture.CultureID);

                    if (isFirst && cultureDTO == null)
                    {
                        cultureDTO = new ProductSKUCultureDataDTO()
                        {
                            CultureID = culture.CultureID,
                            ProductSKUName = dto.SkuName,
                            TimeStamps = dto.TimeStamps
                        };
                        isFirst = false;
                        continue;
                    }
                    skuviewmodel.SkuName.SetValueByCultureID(culture, cultureDTO == null ? string.Empty : cultureDTO.ProductSKUName, cultureDTO == null ? null : cultureDTO.TimeStamps);
                }
            }

            if (dto.ImageMaps != null && dto.ImageMaps.Count > 0)
            {
                foreach (var imageMapDTO in dto.ImageMaps)
                {
                    var imageMapViewModel = new SKUImageMapViewModel();
                    imageMapViewModel.ImageMapID = imageMapDTO.ImageMapID != null ? Convert.ToInt64(imageMapDTO.ImageMapID) : 0;
                    imageMapViewModel.ImageName = imageMapDTO.ImageName;
                    imageMapViewModel.ImagePath = Path.Combine(new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.Products.ToString(), imageMapDTO.ImagePath);
                    imageMapViewModel.ProductImageTypeID = imageMapDTO.ProductImageTypeID != null ? Convert.ToInt64(imageMapDTO.ProductImageTypeID) : 0;
                    imageMapViewModel.Sequence = imageMapDTO.Sequence;
                    imageMapViewModel.SKUMapID = imageMapDTO.SKUMapID != null ? Convert.ToInt64(imageMapDTO.SKUMapID) : 0;
                    imageMapViewModel.ProductID = imageMapDTO.ProductID != null ? Convert.ToInt64(imageMapDTO.ProductID) : 0;
                    skuviewmodel.ImageMaps.Add(imageMapViewModel);
                }
            }

            skuviewmodel.VideoMaps = new List<SKUVideoMapViewModel>();
            if (dto.ProductVideoMaps != null && dto.ProductVideoMaps.Count > 0)
            {
                foreach (var videoMapDTO in dto.ProductVideoMaps)
                {
                    var videoMapViewModel = new SKUVideoMapViewModel();

                    videoMapViewModel.ProductVideoMapID = videoMapDTO.ProductVideoMapID.IsNotNull() ? Convert.ToInt64(videoMapDTO.ProductVideoMapID) : 0;
                    videoMapViewModel.VideoName = videoMapDTO.VideoName;
                    videoMapViewModel.VideoPath = Path.Combine(new Domain.Setting.SettingBL().GetSettingValue<string>("RooImageHostUrltUrl").ToString(), EduegateImageTypes.Products.ToString(), videoMapDTO.VideoFile);
                    videoMapViewModel.Sequence = videoMapDTO.Sequence;
                    videoMapViewModel.ProductSKUMapID = videoMapDTO.ProductSKUMapID != null ? Convert.ToInt64(videoMapDTO.ProductSKUMapID) : 0;
                    videoMapViewModel.ProductID = videoMapDTO.ProductID != null ? Convert.ToInt64(videoMapDTO.ProductID) : 0;

                    skuviewmodel.VideoMaps.Add(videoMapViewModel);
                }
            }


            // Product Inventory SKU Config
            if (dto.ProductInventoryConfigDTOs.IsNotNull())
            {
                skuviewmodel.ProductInventoryConfigViewModels = new ProductInventoryConfigViewModel();
                skuviewmodel.ProductInventoryConfigViewModels = ProductInventoryConfigViewModel.ToViewModel(dto.ProductInventoryConfigDTOs, cultures);
            }

            //SKU Properties
            skuviewmodel.Properties = new List<PropertyViewModel>();
            if (dto.Properties != null && dto.Properties.Count > 0)
            {
                foreach (var productPropertyMap in dto.Properties)
                {
                    var propertyViewModel = new PropertyViewModel();
                    propertyViewModel.DefaultValue = productPropertyMap.DefaultValue;
                    propertyViewModel.PropertyIID = Convert.ToInt32(productPropertyMap.PropertyIID);
                    propertyViewModel.PropertyName = productPropertyMap.PropertyName;
                    skuviewmodel.Properties.Add(propertyViewModel);
                }
            }
            return skuviewmodel;
        }

        public static List<ProductSKUCultureDataDTO> ToCultureDTO(SKUViewModel vm, List<CultureDataInfoViewModel> cultures)
        {
            var dtos = new List<ProductSKUCultureDataDTO>();
            bool isFrist = true;

            foreach (var culture in cultures)
            {
                //Assume that first one is the default culture which will be there by default.
                if (isFrist)
                {
                    isFrist = false;
                    continue;
                }

                dtos.Add(new ProductSKUCultureDataDTO()
                {
                    CultureID = culture.CultureID,
                    ProductSKUMapID = vm.ProductSKUMapID,
                    ProductSKUName = culture.CultureValue
                    // TimeStamps = vm.PageTitle.GetTimeStampByCultureID(culture.CultureID),
                });
            }

            return dtos;
        }

        public static SKUViewModel FromDTOToVM(SKUDTO dto)
        {
            SKUViewModel sVM = new SKUViewModel();
            if (dto.IsNotNull())
            {
                sVM.ProductSKUMapID = dto.ProductSKUMapID;
                sVM.ProductID = dto.ProductID;
                sVM.ProductSKUCode = dto.ProductSKUCode;
                sVM.BarCode = dto.BarCode;
                sVM.PartNumber = dto.PartNumber;
                sVM.SKU = dto.SkuName;
                sVM.StatusID = dto.StatusID;
                sVM.CreatedBy = dto.CreatedBy;
                sVM.CreatedDate = dto.CreatedDate;
                sVM.UpdatedBy = dto.UpdatedBy;
                sVM.UpdatedDate = dto.UpdatedDate;
                sVM.TimeStamps = dto.TimeStamps;
                sVM.ProductPrice = (decimal)dto.ProductPrice;
                sVM.CreatedBy = dto.CreatedBy;
                sVM.UpdatedBy = dto.UpdatedBy;
                sVM.CreatedDate = dto.CreatedDate;
                sVM.UpdatedDate = DateTime.Now;
                sVM.CostPrice = dto.CostPrice;
                sVM.SellingPrice = dto.SellingPrice;
                sVM.Quantity = dto.Quantity;
                if (dto.ProductInventorySKUConfigMaps.IsNotNull())
                {
                    foreach (var config in dto.ProductInventorySKUConfigMaps)
                    {
                        var picVM = new ProductInventorySKUConfigMapViewModel();
                        picVM.ProductInventoryConfigID = config.ProductInventoryConfigID;
                        picVM.ProductSKUMapID = config.ProductSKUMapID;
                        picVM.CreatedBy = config.CreatedBy;
                        picVM.CreatedDate = config.CreatedDate;
                        picVM.TimeStamps = config.TimeStamps;

                        picVM.ProductInventoryConfig.ProductInventoryConfigID = config.ProductInventoryConfig.ProductInventoryConfigID;
                        picVM.ProductInventoryConfig.NotifyQuantity = config.ProductInventoryConfig.NotifyQuantity;
                        picVM.ProductInventoryConfig.MinimumQuantity = config.ProductInventoryConfig.MinimumQuantity;
                        picVM.ProductInventoryConfig.MaximumQuantity = config.ProductInventoryConfig.MaximumQuantity;
                        picVM.ProductInventoryConfig.MinimumQuanityInCart = config.ProductInventoryConfig.MinimumQuanityInCart;
                        picVM.ProductInventoryConfig.MaximumQuantityInCart = config.ProductInventoryConfig.MaximumQuantityInCart;
                        picVM.ProductInventoryConfig.ProductWarranty = config.ProductInventoryConfig.ProductWarranty;
                        picVM.ProductInventoryConfig.IsSerialNumber = config.ProductInventoryConfig.IsSerialNumber;
                        picVM.ProductInventoryConfig.IsSerialNumberRequiredForPurchase = config.ProductInventoryConfig.IsSerialNumberRequiredForPurchase;
                        picVM.ProductInventoryConfig.DeliveryMethod = config.ProductInventoryConfig.DeliveryMethod;
                        picVM.ProductInventoryConfig.ProductWeight = config.ProductInventoryConfig.ProductWeight;
                        picVM.ProductInventoryConfig.ProductLength = config.ProductInventoryConfig.ProductLength;
                        picVM.ProductInventoryConfig.ProductWidth = config.ProductInventoryConfig.ProductWidth;
                        picVM.ProductInventoryConfig.ProductHeight = config.ProductInventoryConfig.ProductHeight;
                        picVM.ProductInventoryConfig.DimensionalWeight = config.ProductInventoryConfig.DimensionalWeight;
                        picVM.ProductInventoryConfig.IsMarketPlace = config.ProductInventoryConfig.IsMarketPlace;
                        picVM.ProductInventoryConfig.HSCode = config.ProductInventoryConfig.HSCode;
                        picVM.ProductInventoryConfig.IsHiddenFromList = config.ProductInventoryConfig.IsHiddenFromList;
                        picVM.ProductInventoryConfig.HideSKU = config.ProductInventoryConfig.HideSKU;
                        picVM.ProductInventoryConfig.IsNonRefundable = config.ProductInventoryConfig.IsNonRefundable;
                        picVM.ProductInventoryConfig.IsSerailNumberAutoGenerated = config.ProductInventoryConfig.IsSerailNumberAutoGenerated;
                        picVM.ProductInventoryConfig.MaxQuantityInCartForVerifiedCustomer = config.ProductInventoryConfig.MaxQuantityInCartForVerifiedCustomer;
                        picVM.ProductInventoryConfig.MaxQuantityInCartForNonVerifiedCustomer = config.ProductInventoryConfig.MaxQuantityInCartForNonVerifiedCustomer;
                        picVM.ProductInventoryConfig.MaxQuantityDuration = config.ProductInventoryConfig.MaxQuantityDuration;
                        picVM.ProductInventoryConfig.TimeStamps = config.ProductInventoryConfig.TimeStamps;
                       // picVM.ProductInventoryConfig.EmployeeID = config.ProductInventoryConfig.EmployeeID;
                        sVM.ProductInventorySKUConfigMaps.Add(picVM);
                    }
                }
            }
            return sVM;
        }
    }
}