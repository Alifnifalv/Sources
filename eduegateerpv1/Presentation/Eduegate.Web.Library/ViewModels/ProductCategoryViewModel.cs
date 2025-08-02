using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ProductCategory", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class ProductCategoryViewModel : BaseMasterViewModel
    {
        public ProductCategoryViewModel()
        {
            //PriceListsMap = new PirceListsMap();
            //ImageMap = new ImageMapViewModel() { Maps = new List<ImageTypeMapViewModel>() { new ImageTypeMapViewModel() } };
            Tags = new List<KeyValueViewModel>();
            //CategoryMarketPlace = new CategoryMarketPlaceViewModel();
            //CategoryName = new MultiLanguageText();
            //CategorySettingMap = new CategorySettingsMapViewModel();// { Maps = new List<CategorySettingsViewModel>() { new CategorySettingsViewModel() } };
            LoadCategoryLookUp = true;
        }


       /// [ControlType(Framework.Enums.ControlTypes.Label)]
      ///  [DisplayName("Category ID")]
        public long CategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CategoryCode")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string CategoryCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CategoryName")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string CategoryName { get; set; }

        //[IgnoreMap]
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBoxWithMultiLanguage)]
        //[MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        //[DisplayName("Category Name")]
        //public MultiLanguageText CategoryName { get; set; }


        public string ParentCategoryName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.Category")]
        [CustomDisplay("ParentCategory")]
        [Select2("ParentCategory", "Numeric", false, optionalAttribute1: "ng-click=LoadCategoryLookup('Category',CRUDModel.ViewModel)")]
        public KeyValueViewModel ParentCategory { get; set; }
        public Nullable<long> ParentCategoryID { get; set; }

        //public string ImageName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        //[DisplayName("Thumbnail File")]
        //[FileUploadInfo("Mutual/UploadImages", WBImageTypes.Category, "ThumbnailUrl")]
        //public string ThumbnailImageName { get; set; }
        //public string ThumbnailUrl { get; set; }

        public int Level { get; set; }
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Active")]
        public bool Active { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.CategoryTags")]
        [CustomDisplay("Tags")]
        [Select2("Tags", "String", true, "")]
        public List<KeyValueViewModel> Tags { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "", "ng-click='IsLoadCategoryLookUp()'")]
        [CustomDisplay("IsReporting")]
        public bool IsReporting { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("NavigationMenu")]
        public bool IsInNavigationMenu { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "PriceListsMap", "PriceListsMap")]
        //[DisplayName("Price List")]
        //public PirceListsMap PriceListsMap { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "ImageMap", "ImageMap")]
        //[DisplayName("Image Maps")]
        //public ImageMapViewModel ImageMap { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "CategoryMarketPlaceViewModel", "CategoryMarketPlace")]
        //[DisplayName("Market Place")]
        //public CategoryMarketPlaceViewModel CategoryMarketPlace { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "CategorySettingMap", "CategorySettingMap")]
        //[DisplayName("Category Settings")]
        //public CategorySettingsMapViewModel CategorySettingMap { get; set; }

        public bool LoadCategoryLookUp { get; set; }

        public static ProductCategoryViewModel FromDTO(ProductCategoryDTO dto, List<CultureDataInfoViewModel> datas)
        {
            var vm = FromDTO(dto);
            bool isFirst = true;
            //vm.CategoryName = new MultiLanguageText(datas);
            foreach (var cultureData in datas)
            {
                var data = dto.CategoryCultureDatas.FirstOrDefault(a => a.CultureID == cultureData.CultureID);

                if (isFirst && data == null)
                {
                    data = new CategoryCultureDataDTO()
                    {
                        CultureID = cultureData.CultureID,
                        CategoryName = dto.CategoryName,
                    };

                    isFirst = false;
                }

                //if (data != null)
                //    vm.CategoryName.SetValueByCultureID(cultureData, data.CategoryName, cultureData.TimeStamps);
            }

            return vm;
        }

        public static ProductCategoryViewModel FromDTO(ProductCategoryDTO dto)
        {
            Mapper<ProductCategoryDTO, ProductCategoryViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<CategoryMarketPlaceDTO, CategoryMarketPlaceViewModel>.CreateMap();

            var mapper = Mapper<ProductCategoryDTO, ProductCategoryViewModel>.Map(dto);
            if (dto.ParentCategoryID.HasValue)
            {
                mapper.ParentCategory = new KeyValueViewModel() { Key = dto.ParentCategoryID.Value.ToString(), Value = dto.ParentCategoryName };
            }
            
            //mapper.ImageMap = new ImageMapViewModel();
            //mapper.ImageMap.Maps = new List<ImageTypeMapViewModel>();

            //foreach (var imageMap in dto.ImageMaps)
            //{
            //    mapper.ImageMap.Maps.Add(new ImageTypeMapViewModel()
            //    {
            //        ImageMapIID = imageMap.CategoryImageMapIID,
            //        ImageName = imageMap.ImageFile,
            //        ImageTitle = imageMap.ImageTitle,
            //        ImageLinkParameters = imageMap.ImageLinkParameters,
            //        ImageTarget = imageMap.ImageTarget,
            //        TimeStamps = imageMap.TimeStamps,
            //        ImageType = imageMap.ImageType.HasValue ? imageMap.ImageType.Value.ToString() : null,
            //        ActionLinkTypeID = imageMap.ActionLinkTypeID > 0 ? imageMap.ActionLinkTypeID.ToString() : null,
            //        SerialNo = imageMap.SerialNo,
            //    });
            //}
            
            //foreach (var setting in dto.CategorySetting)
            //{
            //    mapper.CategorySettingMap.Maps.Add(new CategorySettingsViewModel()
            //    {
            //        CategorySettingsID = setting.CategorySettingsID,
            //        CategoryID = setting.CategoryID.HasValue?setting.CategoryID.Value:default(long),
            //        SettingCode = setting.SettingCode,
            //        SettingValue = setting.SettingValue,
            //        Description = setting.Description,
            //        CreatedBy = setting.CreatedBy,
            //        CreatedDate = setting.CreatedDate,
            //        UpdatedBy = setting.UpdatedBy,
            //        UpdatedDate = setting.UpdatedDate
            //    });
            //}

            return mapper;
        }

        //public override void InitializeVM(List<CultureDataInfoViewModel> datas)
        //{
            //CategoryName.CultureDatas = datas;
            //base.InitializeVM(datas);
        //}

        public static ProductCategoryDTO ToDTO(ProductCategoryViewModel vm, List<CultureDataInfoViewModel> datas)
        {
            var dto = ToDTO(vm);
            dto.CategoryCultureDatas = ToCultureDTO(vm, datas);
            return dto;
        }

        public static List<CategoryCultureDataDTO> ToCultureDTO(ProductCategoryViewModel vm, List<CultureDataInfoViewModel> cultures)
        {
            var dtos = new List<CategoryCultureDataDTO>();
            bool isFrist = true;

            foreach (var culture in cultures)
            {
                //Assume that first one is the default culture which will be there by default.
                if (isFrist)
                {
                    isFrist = false;
                    continue;
                }

                dtos.Add(new CategoryCultureDataDTO()
                {
                    CultureID = culture.CultureID,
                    CategoryID = vm.CategoryID,
                    //CategoryName = vm.CategoryName.GetValueByCultureID(culture.CultureID),
                    //TimeStamps = vm.CategoryName.GetTimeStampByCultureID(culture.CultureID),
                });
            }

            return dtos;
        }

        public static ProductCategoryDTO ToDTO(ProductCategoryViewModel vm)
        {
            Mapper<ProductCategoryViewModel, ProductCategoryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<CategoryMarketPlaceViewModel, CategoryMarketPlaceDTO>.CreateMap();

            var mapper = Mapper<ProductCategoryViewModel, ProductCategoryDTO>.Map(vm);

            if (vm.ParentCategory != null && !string.IsNullOrEmpty(vm.ParentCategory.Key))
            {
                mapper.ParentCategoryID = long.Parse(vm.ParentCategory.Key);
            }
            
            mapper.ImageMaps = new List<Services.Contracts.Catalog.CategoryImageMapDTO>();

            //foreach (var imageMap in vm.ImageMap.Maps)
            //{
            //    if (imageMap.ImageType == null) continue;

            //    mapper.ImageMaps.Add(new Services.Contracts.Catalog.CategoryImageMapDTO()
            //    {
            //        CategoryImageMapIID = imageMap.ImageMapIID,
            //        CategoryID = mapper.CategoryID,
            //        ImageType = (ImageTypes)Enum.Parse(typeof(ImageTypes), imageMap.ImageType),
            //        ImageTitle = imageMap.ImageTitle,
            //        ImageLinkParameters = imageMap.ImageLinkParameters,
            //        ImageTarget = imageMap.ImageTarget,
            //        ImageFile = imageMap.ImageName,
            //        TimeStamps = imageMap.TimeStamps,
            //        ActionLinkTypeID = string.IsNullOrWhiteSpace(imageMap.ActionLinkTypeID) ? 0 : Convert.ToInt32(imageMap.ActionLinkTypeID),
            //        SerialNo = imageMap.SerialNo,
            //    });
            //}

            //foreach (var setting in vm.CategorySettingMap.Maps)
            //{
            //    mapper.CategorySetting.Add( new CategorySettingDTO() { 
            //        CategorySettingsID = setting.CategorySettingsID,
            //        CategoryID = setting.CategoryID,
            //        SettingCode = setting.SettingCode,
            //        SettingValue = setting.SettingValue,
            //        Description = setting.Description,
            //        CreatedBy = setting.CreatedBy,
            //        CreatedDate = setting.CreatedDate,
            //        UpdatedBy = setting.UpdatedBy,
            //        UpdatedDate = setting.UpdatedDate
            //    });
                
            //}

            return mapper;
        }       
    }
}