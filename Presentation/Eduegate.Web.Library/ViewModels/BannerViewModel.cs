using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class BannerViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Banner ID")]
        public long BannerIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled='CRUDModel.ViewModel.BannerIID == 0'")]
        [DisplayName("Banner Sequence")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Banner Name")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string BannerName { get; set; }

        // Existing File Upload for Banner File
        [Required]

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("Banner File")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "ProfileUrl", "")]
        public string ProfileUrl { get; set; }

        public string BannerUrl { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.BannerType")]
        [DisplayName("Banner Type")]
        public string BannerTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Reference ID")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ReferenceID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Frequency")]
        public int Frequency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.ActionLinkType")]
        [DisplayName("ActionLinkType")]
        public string ActionLinkTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(300, ErrorMessage = "Maximum Length should be within 300!")]
        [DisplayName("ActionParameters")]
        public string BannerActionLinkParameters { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Target")]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        public string Target { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.BannerStatus")]
        [DisplayName("Status")]
        public string StatusID { get; set; }
        public Nullable<int> CompanyID { get; set; }

        public static BannerViewModel FromDTO(BannerMasterDTO dto)
        {
            return new BannerViewModel()
            {
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate ?? DateTime.MinValue,
                UpdatedDate = dto.UpdatedDate ?? DateTime.MinValue,
                BannerIID = dto.BannerIID,
                ProfileUrl =  dto.BannerFile,
                BannerUrl = dto.BannerFile ?? string.Empty,
                BannerName = dto.BannerName ?? string.Empty,
                BannerTypeID = dto.BannerTypeID.HasValue ? dto.BannerTypeID.Value.ToString() : "0",
                Frequency = dto.Frequency,
                BannerActionLinkParameters = dto.Link ?? string.Empty,
                TimeStamps = dto.TimeStamps ?? string.Empty,
                Target = dto.Target ?? string.Empty,
                StatusID = dto.StatusID.ToString(),
                ReferenceID = dto.ReferenceID ?? string.Empty,
                ActionLinkTypeID = dto.ActionLinkTypeID > 0 ? dto.ActionLinkTypeID.ToString() : null,
                SerialNo = dto.SerialNo,
                CompanyID = dto.CompanyID,
            };
        }

        public static BannerMasterDTO ToDTO(BannerViewModel vm)
        {
            return new BannerMasterDTO()
            {
                BannerFile = vm.ProfileUrl,
                BannerIID = vm.BannerIID,
                BannerName = vm.BannerName ?? string.Empty,
                BannerTypeID = int.TryParse(vm.BannerTypeID, out var bannerTypeID) ? bannerTypeID : 0,
                Frequency = (byte)vm.Frequency,
                Link = vm.BannerActionLinkParameters ?? string.Empty,
                StatusID = int.TryParse(vm.StatusID, out var statusID) ? statusID : 0,
                Target = vm.Target ?? string.Empty,
                TimeStamps = vm.TimeStamps ?? string.Empty,
                ReferenceID = vm.ReferenceID ?? string.Empty,
                ActionLinkTypeID = string.IsNullOrWhiteSpace(vm.ActionLinkTypeID) ? 0 : int.Parse(vm.ActionLinkTypeID),
                SerialNo = vm.SerialNo,
                CompanyID = vm.CompanyID ?? default(int),
            };
        }
    }
}