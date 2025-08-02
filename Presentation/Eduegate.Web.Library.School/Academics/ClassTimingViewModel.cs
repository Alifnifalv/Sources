using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework.Translator;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class ClassTimingViewModel : BaseMasterViewModel
    {
        public ClassTimingViewModel()
        {
            //ClassTimingSets = new KeyValueViewModel();
            IsBreakTime = false;
        }

        public int ClassTimingID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.ClassTimingSets")]
        [CustomDisplay("ClassShift")]
        public string ClassTimingSets { get; set; }
        public int? ClassTimingSetID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string TimingDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("StartTime")]
        public string StartTimeString { get; set; }
        //public TimeSpan? StartTime { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("EndTime")]
        public string EndTimeString { get; set; }
        //public TimeSpan? EndTime { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsBreakTime")]
        public bool? IsBreakTime { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled=!CRUDModel.ViewModel.IsBreakTime")]
        [LookUp("LookUps.BreakTypes")]
        [CustomDisplay("BreakType")]
        public string BreakType { get; set; }
        public byte? BreakTypeID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassTimingDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassTimingViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassTimingDTO, ClassTimingViewModel>.CreateMap();
            ClassTimingDTO timeDTO = dto as ClassTimingDTO;
            var vm = Mapper<ClassTimingDTO, ClassTimingViewModel>.Map(timeDTO);

            vm.ClassTimingSets = timeDTO.ClassTimingSetID.ToString();
            vm.StartTimeString = timeDTO.StartTime.HasValue ? DateTime.Today.Add(timeDTO.StartTime.Value).ToString("hh:mm tt") : null;
            vm.EndTimeString = timeDTO.EndTime.HasValue ? DateTime.Today.Add(timeDTO.EndTime.Value).ToString("hh:mm tt") : null;
            vm.BreakType = timeDTO.BreakTypeID.HasValue ? timeDTO.BreakTypeID.ToString() : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassTimingViewModel, ClassTimingDTO>.CreateMap();
            var dto = Mapper<ClassTimingViewModel, ClassTimingDTO>.Map(this);

            dto.ClassTimingSetID = string.IsNullOrEmpty(this.ClassTimingSets) ? (int?)null : int.Parse(this.ClassTimingSets);
            dto.StartTime = this == null || this.StartTimeString == null || this.StartTimeString == "" ? (TimeSpan?)null : DateTime.Parse(this.StartTimeString).TimeOfDay;
            dto.EndTime = this == null || this.EndTimeString == null || this.EndTimeString == "" ? (TimeSpan?)null : DateTime.Parse(this.EndTimeString).TimeOfDay;
            dto.BreakTypeID = string.IsNullOrEmpty(this.BreakType) || this.BreakType == "" ? (byte?)null : byte.Parse(this.BreakType);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassTimingDTO>(jsonString);
        }
    }
}
