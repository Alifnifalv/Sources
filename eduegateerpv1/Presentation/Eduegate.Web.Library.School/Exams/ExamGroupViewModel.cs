using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExamGroup", "CRUDModel.ViewModel")]
    [DisplayName("Exam Group")]
    public class ExamGroupViewModel : BaseMasterViewModel
    {
        public ExamGroupViewModel()
        {
            IsActive = true;
        }

        public long ExamGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AssessmentTerm")]
        public string ExamGroupName { get; set; }

        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("StartDate")]
        public string FromDateString { get; set; }
        public DateTime? FromDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("EndDate")]
        public string ToDateString { get; set; }
        public DateTime? ToDate { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ExamGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ExamGroupDTO, ExamGroupViewModel>.CreateMap();
            var exmdto = dto as ExamGroupDTO;
            var vm = Mapper<ExamGroupDTO, ExamGroupViewModel>.Map(exmdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.FromDateString = exmdto.FromDate.HasValue ? Convert.ToDateTime(exmdto.FromDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ToDateString = exmdto.ToDate.HasValue ? Convert.ToDateTime(exmdto.ToDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ExamGroupViewModel, ExamGroupDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<ExamGroupViewModel, ExamGroupDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.FromDate = string.IsNullOrEmpty(this.FromDateString) ? (DateTime?)null : DateTime.ParseExact(this.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ToDate = string.IsNullOrEmpty(this.ToDateString) ? (DateTime?)null : DateTime.ParseExact(this.ToDateString, dateFormat, CultureInfo.InvariantCulture);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExamGroupDTO>(jsonString);
        }
    }
}

