using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Galleries;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Gallery;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.School.Galleries
{
    public class GalleryViewModel : BaseMasterViewModel
    {
        public GalleryViewModel()
        {
            GalleryAttachments = new List<GalleryAttachmentMapViewModel>() { new GalleryAttachmentMapViewModel() };
            AcademicYear = new KeyValueViewModel();
            IsActive = false;
        }

        public long GalleryIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Activity Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Activity Date")]

        public string GalleryDateString { get; set; }
        public DateTime? GalleryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Activity to be displayed From")]

        public string StartDateString { get; set; }
        public DateTime? StartDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Activity to be displayed To")]

        public string ExpiryDateString { get; set; }
        public DateTime? ExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Attachment")]
        public List<GalleryAttachmentMapViewModel> GalleryAttachments { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine3 { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as GalleryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<GalleryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var GllryDTO = dto as GalleryDTO;
            Mapper<GalleryDTO, GalleryViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var vm = Mapper<GalleryDTO, GalleryViewModel>.Map(GllryDTO);
            var GlryDto = dto as GalleryDTO;

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.AcademicYearID = GlryDto.AcademicYearID;
            vm.AcademicYear = GlryDto.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = GlryDto.AcademicYear.Key, Value = GlryDto.AcademicYear.Value } : new KeyValueViewModel();
            vm.GalleryIID = GlryDto.GalleryIID;
            vm.GalleryDateString = GlryDto.GalleryDate.HasValue ? GlryDto.GalleryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.StartDateString = GlryDto.StartDate.HasValue ? GlryDto.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ExpiryDateString = GlryDto.ExpiryDate.HasValue ? GlryDto.ExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Description = GlryDto.Description;
            vm.IsActive = GllryDTO.ISActive;

            vm.GalleryAttachments = new List<GalleryAttachmentMapViewModel>();

            if (GlryDto.GalleryAttachmentMaps != null)
            {
                foreach (var attach in GlryDto.GalleryAttachmentMaps)
                {
                    if (attach.AttachmentContentID.HasValue || !string.IsNullOrEmpty(attach.AttachmentName))
                    {
                        vm.GalleryAttachments.Add(new GalleryAttachmentMapViewModel
                        {
                            GalleryAttachmentMapIID = attach.GalleryAttachmentMapIID,
                            GalleryID = attach.GalleryID,
                            ContentFileName = attach.AttachmentName,
                            AttachmentContentID = attach.AttachmentContentID,
                        });
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<GalleryViewModel, GalleryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<GalleryViewModel, GalleryDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.ISActive = this.IsActive;
            dto.GalleryIID = this.GalleryIID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            dto.GalleryDate = string.IsNullOrEmpty(this.GalleryDateString) ? (DateTime?)null : DateTime.ParseExact(this.GalleryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.StartDate = string.IsNullOrEmpty(this.StartDateString) ? (DateTime?)null : DateTime.ParseExact(this.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ExpiryDate = string.IsNullOrEmpty(this.ExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(this.ExpiryDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.Description = this.Description;

            dto.GalleryAttachmentMaps = new List<GalleryAttachmentMapDTO>();

            if (this.GalleryAttachments.Count > 0)
            {
                foreach (var attachment in this.GalleryAttachments)
                {
                    if (attachment.AttachmentContentID.HasValue || !string.IsNullOrEmpty(attachment.ContentFileName))
                    {
                        dto.GalleryAttachmentMaps.Add(new GalleryAttachmentMapDTO
                        {
                            GalleryAttachmentMapIID = attachment.GalleryAttachmentMapIID,
                            GalleryID = attachment.GalleryID.HasValue ? attachment.GalleryID : null,
                            AttachmentContentID = attachment.AttachmentContentID,
                            AttachmentName = attachment.ContentFileName,
                            //AttachmentDescription = attachment.AttachmentDescription,
                            CreatedBy = attachment.CreatedBy,
                            UpdatedBy = attachment.UpdatedBy,
                            CreatedDate = attachment.CreatedDate,
                            UpdatedDate = attachment.UpdatedDate,
                        });
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<GalleryDTO>(jsonString);
        }
    }
}

