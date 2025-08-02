using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Mutual
{
    public class AcademicYearViewModel : BaseMasterViewModel
    {
        public AcademicYearViewModel()
        {
            IsActive = false;
        }

        public int  AcademicYearID { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AcademicYearCode")]
        public string  AcademicYearCode { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("StartDate")]
        public string StartDateString { get; set; }
        public System.DateTime?  StartDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("EndDate")]
        public string EndDateString { get; set; }
        public System.DateTime?  EndDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("School")]
        [LookUp("LookUps.School")]
        public string  School { get; set; }
        public byte? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.AcademicYearStatus")]
        public string AcademicYearStatus { get; set; }
        public byte? AcademicYearStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("OrderNumber")]
        public int ORDERNO { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AcademicYearDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AcademicYearViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AcademicYearDTO, AcademicYearViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var acDtO = dto as AcademicYearDTO;
            var vm = Mapper<AcademicYearDTO, AcademicYearViewModel>.Map(dto as AcademicYearDTO);
            vm.School = acDtO.SchoolID.ToString();
            vm.AcademicYearStatus = acDtO.AcademicYearStatusID.ToString();
            vm.StartDateString = acDtO.StartDate.HasValue ? acDtO.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.EndDateString = acDtO.EndDate.HasValue ? acDtO.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.IsActive = acDtO.IsActive;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AcademicYearViewModel, AcademicYearDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<AcademicYearViewModel, AcademicYearDTO>.Map(this);
            dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            dto.AcademicYearStatusID = string.IsNullOrEmpty(this.AcademicYearStatus) ? (byte?)null : byte.Parse(this.AcademicYearStatus);
            dto.StartDate = string.IsNullOrEmpty(this.StartDateString) ? (DateTime?)null : DateTime.ParseExact(this.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EndDate = string.IsNullOrEmpty(this.EndDateString) ? (DateTime?)null : DateTime.ParseExact(this.EndDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.IsActive = this.IsActive;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AcademicYearDTO>(jsonString);
        }
    }
}