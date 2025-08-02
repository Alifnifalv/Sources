using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Brand", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class BrandViewModel : BaseMasterViewModel
    {
        public BrandViewModel()
        {
            Tags = new List<KeyValueViewModel>();
            //ImageMap = new ImageMapViewModel() { Maps = new List<ImageTypeMapViewModel>() { new ImageTypeMapViewModel() } };
        }

        public long BrandIID { get; set; }

        [Required]
        // @ng-unique is a angular drective
        // @Param1 is attribute which will take parameter name (brandName) for this method (Brand/BrandNameAvailibility)
        // @ControllerCall is attribute which define our ControllerName/MethodName in html
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-unique param1=brandName ControllerCall=Brand/BrandNameAvailibility")]
        [CustomDisplay("BrandName")]
        [MaxLength(50)]
        public string BrandName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.RichTextEditor)]
        [MaxLength(1000)]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        //[DisplayName("Logo")]
        //[FileUploadInfo("Mutual/UploadImages", EduegateImageTypes.Brands, "ImageUrl")]
        //public string Logo { get; set; }
        //public string ImageUrl { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.BrandStatus")]
        [CustomDisplay("Status")]
        public string StatusID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.BrandTags")]
        [CustomDisplay("Tags")]
        [Select2("Tags", "String", true, null, true)]
        public List<KeyValueViewModel> Tags { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Apply status to all catalog/Sku's ")]
        //public Nullable<bool> Status { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "ImageMap", "ImageMap")]
        //[DisplayName("Image Maps")]
        //public ImageMapViewModel ImageMap { get; set; }

        public static BrandDTO ToDTO(BrandViewModel brand)
        {
            var brandDTO = new BrandDTO();
            brandDTO.BrandIID = brand.BrandIID;
            brandDTO.BrandName = brand.BrandName;
            brandDTO.Description = brand.Description;
            brandDTO.StatusID = Convert.ToInt32(brand.StatusID);
            //brandDTO.BrandLogo = brand.Logo;
            brandDTO.Tags = new List<KeyValueDTO>();
            brandDTO.TimeStamps = brand.TimeStamps;
            //brandDTO.Status = brand.Status;

            if (brand.Tags != null)
            {
                foreach (var tag in brand.Tags)
                {
                    brandDTO.Tags.Add(KeyValueViewModel.ToDTO(tag));
                }
            }

            brandDTO.ImageMaps = new  List<BrandImageMapDTO>();

            //foreach (var imageMap in brand.ImageMap.Maps)
            //{
            //    if (imageMap.ImageType == null) continue;
 
            //    brandDTO.ImageMaps.Add(new Services.Contracts.Catalog.BrandImageMapDTO()
            //    {
            //        BrandImageMapIID = imageMap.ImageMapIID,
            //        BrandID = brand.BrandIID,
            //        ImageType = (ImageTypes)Enum.Parse(typeof(ImageTypes), imageMap.ImageType),
            //        ImageTitle = imageMap.ImageTitle,
            //        ImageFile = imageMap.ImageName,
            //        TimeStamps = imageMap.TimeStamps,
            //    });
            //}

            return brandDTO;
        }

        public static BrandViewModel FromDTO(BrandDTO brandDTO)
        {
            var brandViewmodel = new BrandViewModel();
            brandViewmodel.BrandIID = brandDTO.BrandIID;
            brandViewmodel.BrandName = brandDTO.BrandName;
            brandViewmodel.Description = brandDTO.Description;
            brandViewmodel.StatusID = Convert.ToString(brandDTO.StatusID);
            //brandViewmodel.Logo = string.Format("{0}{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.Brands.ToString(), brandDTO.BrandLogo);
            //brandViewmodel.ImageUrl = brandViewmodel.Logo;
            brandViewmodel.TimeStamps = brandDTO.TimeStamps;
            //brandViewmodel.Status = brandDTO.Status;
            brandViewmodel.IsError = brandDTO.IsError;
            brandViewmodel.ErrorCode = brandDTO.ErrorCode;

            if (brandDTO.Tags != null)
            {
                foreach (var tag in brandDTO.Tags)
                {
                    brandViewmodel.Tags.Add(KeyValueViewModel.ToViewModel(tag));
                }
            }

            //brandViewmodel.ImageMap = new ImageMapViewModel();
            //brandViewmodel.ImageMap.Maps = new List<ImageTypeMapViewModel>();

            //if (brandDTO.ImageMaps != null)
            //{
            //    foreach (var imageMap in brandDTO.ImageMaps)
            //    {
            //        brandViewmodel.ImageMap.Maps.Add(new ImageTypeMapViewModel()
            //        {
            //            ImageMapIID = imageMap.BrandImageMapIID,
            //            ImageName = imageMap.ImageFile,
            //            ImageTitle = imageMap.ImageTitle,
            //            TimeStamps = imageMap.TimeStamps,
            //            ImageType = imageMap.ImageType.HasValue ? imageMap.ImageType.Value.ToString() : null,
            //        });
            //    }
            //}
            //else
            //{
            //    brandViewmodel.ImageMap.Maps.Add(new ImageTypeMapViewModel());
            //}

            //foreach (var mapVm in brandViewmodel.ImageMap.Maps)
            //{
            //    mapVm.ImageUrl = string.Format("{0}//{1}//{2}//{3}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.Brands.ToString(),
            //        mapVm.ImageType == null ? string.Empty : mapVm.ImageType.ToString(), mapVm.ImageName);
            //}

            return brandViewmodel;
        }
    }
}