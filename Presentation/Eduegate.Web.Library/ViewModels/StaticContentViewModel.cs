using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Eduegate.Domain;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class StaticContentViewModel : BaseMasterViewModel
    {
        public long ContentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.StaticContentType")]
        [CustomDisplay("ContentType")]
        public string ContentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ContentTitle")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string ContentTitle { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Product ID")]
        public string ProductID { get; set; }

        public string ImageUrl { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("ImageFile")]
        [FileUploadInfo("Mutual/UploadImages", Framework.Enums.EduegateImageTypes.Styled, "ImageUrl", "")]
        public string ImageFile { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Brands")]
        [CustomDisplay("Designer")]
        public string BrandID { get; set; }

        public static StaticContentDTO ToDTO(StaticContentViewModel staticContentViewModel)
        {
            StaticContentDTO contentDTO = new StaticContentDTO();
            contentDTO.ContentDataIID = staticContentViewModel.ContentID;
            contentDTO.ContentTypeID = (Eduegate.Services.Contracts.Enums.StaticContentTypes)int.Parse(staticContentViewModel.ContentType);
            contentDTO.Title = staticContentViewModel.ContentTitle;
            contentDTO.Description = staticContentViewModel.Description;
            contentDTO.ImageFilePath = staticContentViewModel.ImageUrl;
            contentDTO.AdditionalParameters = new List<Services.Contracts.Commons.KeyValueParameterDTO>();
            contentDTO.AdditionalParameters.Add(new Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Enums.StaticContentType.ByBrand.Keys.ProductID, ParameterValue = staticContentViewModel.ProductID });
            //we should not send the designer if the content type is 'By Brand'
            if( (int)contentDTO.ContentTypeID == 2)
            {
                contentDTO.AdditionalParameters.Add(new Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Enums.StaticContentType.ByEduegates.Keys.BrandID, ParameterValue = staticContentViewModel.BrandID });
            }
            return contentDTO;
        }

        public static StaticContentViewModel FromDTO(StaticContentDTO contentDTO)
        {
            StaticContentViewModel model = new StaticContentViewModel();
            model.ContentID = contentDTO.ContentDataIID;
            model.ContentType = ((int)contentDTO.ContentTypeID).ToString();
            model.ContentTitle = contentDTO.Title;
            model.Description = contentDTO.Description;
            model.ImageFile =  string.Format("{0}\\{1}\\{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString(), EduegateImageTypes.Styled.ToString(), contentDTO.ImageFilePath);
            model.ImageUrl = model.ImageFile;
            model.ProductID = string.Join(",", contentDTO.AdditionalParameters.Where(x => x.ParameterName == Eduegate.Services.Contracts.Enums.StaticContentType.ByBrand.Keys.ProductID).Select(y => y.ParameterValue).ToArray());
            model.BrandID = string.Join(",", contentDTO.AdditionalParameters.Where(x => x.ParameterName == Eduegate.Services.Contracts.Enums.StaticContentType.ByEduegates.Keys.BrandID).Select(y => y.ParameterValue).FirstOrDefault());
            model.CreatedDate = contentDTO.CreatedDate;
            model.CreatedBy = contentDTO.CreatedBy;
            model.UpdatedDate = contentDTO.UpdatedDate;
            model.UpdatedBy = Convert.ToInt32(contentDTO.UpdatedBy);
            return model;
        }
    }
}