using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.Lms.Lms
{
    public class LmsGroupViewModel : BaseMasterViewModel
    {
        public LmsGroupViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            IsActive = true;
            FromDateString = DateTime.Now.Date.ToString(dateFormat, CultureInfo.InvariantCulture);
        }

        public int SignupGroupID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string GroupTitle { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [CustomDisplay("Description")]
        public string GroupDescription { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled='CRUDModel.ViewModel.SignupGroupID > 0'")]
        [CustomDisplay("FromDate")]
        public string FromDateString { get; set; }
        public DateTime? FromDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("ToDate")]
        public string ToDateString { get; set; }
        public DateTime? ToDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DueDate")]
        public string DueDateString { get; set; }
        public DateTime? DueDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SignUpGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LmsGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SignUpGroupDTO, LmsGroupViewModel>.CreateMap();
            var grpDTO = dto as SignUpGroupDTO;
            var vm = Mapper<SignUpGroupDTO, LmsGroupViewModel>.Map(dto as SignUpGroupDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.SignupGroupID = grpDTO.SignupGroupID;
            vm.IsActive = grpDTO.IsActive ?? false;
            vm.FromDateString = grpDTO.FromDate.HasValue ? grpDTO.FromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ToDateString = grpDTO.ToDate.HasValue ? grpDTO.ToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.DueDateString = grpDTO.DueDate.HasValue ? grpDTO.DueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LmsGroupViewModel, SignUpGroupDTO>.CreateMap();
            var dto = Mapper<LmsGroupViewModel, SignUpGroupDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.SignupGroupID = this.SignupGroupID;
            dto.IsActive = this.IsActive;
            dto.FromDate = string.IsNullOrEmpty(this.FromDateString) ? (DateTime?)null : DateTime.ParseExact(this.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ToDate = string.IsNullOrEmpty(this.ToDateString) ? (DateTime?)null : DateTime.ParseExact(this.ToDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.DueDate = string.IsNullOrEmpty(this.DueDateString) ? (DateTime?)null : DateTime.ParseExact(this.DueDateString, dateFormat, CultureInfo.InvariantCulture);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SignUpGroupDTO>(jsonString);
        }

    }
}