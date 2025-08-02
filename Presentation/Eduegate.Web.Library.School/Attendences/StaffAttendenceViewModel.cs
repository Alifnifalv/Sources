using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Attendences;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Attendences
{
    public class StaffAttendenceViewModel : BaseMasterViewModel
    {
        public StaffAttendenceViewModel()
        {
            StaffAttendance = new StaffAttendanceAttendanceViewModel();
        }

        public long  StaffAttendenceIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        [CustomDisplay("Staff")]
        public KeyValueViewModel Employee { get; set; }
        public long? EmployeeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [DisplayName("")]
        public StaffAttendanceAttendanceViewModel StaffAttendance { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("StartTime")]
        public string StartTimeString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("EndTime")]
        public string EndTimeString { get; set; }    
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StaffAttendenceDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StaffAttendenceViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StaffAttendenceDTO, StaffAttendenceViewModel>.CreateMap();
            var attendenceDTO = dto as StaffAttendenceDTO;
            var vm = Mapper<StaffAttendenceDTO, StaffAttendenceViewModel>.Map(attendenceDTO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.StartTimeString = attendenceDTO.StartTime.HasValue ? DateTime.Today.Add(attendenceDTO.StartTime.Value).ToString("hh:mm tt") : null;
            vm.EndTimeString = attendenceDTO.EndTime.HasValue ? DateTime.Today.Add(attendenceDTO.EndTime.Value).ToString("hh:mm tt") : null;
            vm.StaffAttendance.PresentStatus = attendenceDTO.PresentStatusID.HasValue ? attendenceDTO.PresentStatusID.ToString() : null;
            vm.Employee = attendenceDTO.EmployeeID.HasValue ? new KeyValueViewModel() { Key = attendenceDTO.EmployeeID.ToString(), Value = attendenceDTO.EmployeeName } : new KeyValueViewModel();
            vm.StaffAttendance.AttendenceDateString = attendenceDTO.AttendenceDate.HasValue ? attendenceDTO.AttendenceDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.StaffAttendance.Notes = attendenceDTO.Reason;
            vm.StaffAttendance.AttendenceReasonID = attendenceDTO.AttendenceReasonID;
            vm.StaffAttendance.AttendenceReason = attendenceDTO.AttendenceReasonID.HasValue ? attendenceDTO.AttendenceReasonID.Value.ToString() : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StaffAttendenceViewModel, StaffAttendenceDTO>.CreateMap();
            var dto = Mapper<StaffAttendenceViewModel, StaffAttendenceDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.StartTime = this == null || this.StartTimeString == null || this.StartTimeString == "" ? (TimeSpan?)null : DateTime.Parse(this.StartTimeString).TimeOfDay;
            dto.EndTime = this == null || this.EndTimeString == null || this.EndTimeString == "" ? (TimeSpan?)null : DateTime.Parse(this.EndTimeString).TimeOfDay;
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (int?)null : int.Parse(this.Employee.Key);
            dto.PresentStatusID = string.IsNullOrEmpty(this.StaffAttendance.PresentStatus) ? (byte?)null : byte.Parse(this.StaffAttendance.PresentStatus);
            dto.AttendenceDate = string.IsNullOrEmpty(this.StaffAttendance.AttendenceDateString) ? (DateTime?)null : DateTime.ParseExact(StaffAttendance.AttendenceDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.Reason = this.StaffAttendance.Notes;
            dto.AttendenceReasonID = this.StaffAttendance.AttendenceReasonID;
            dto.AttendenceReasonID = string.IsNullOrEmpty(this.StaffAttendance.AttendenceReason) ? (byte?)null : byte.Parse(this.StaffAttendance.AttendenceReason);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StaffAttendenceDTO>(jsonString);
        }
    }
}