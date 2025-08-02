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
    public class StudentAttendenceViewModel : BaseMasterViewModel
    {
        public StudentAttendenceViewModel()
        {
            //Student = new KeyValueViewModel();
            Attendence = new AttendenceViewModel();
            //Section = new KeyValueViewModel();
            //Class = new KeyValueViewModel();
        }

        public long  StudentAttendenceIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MainTeacherClasses", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MainTeacherSections", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MainTeacherStudents", "Numeric", false, "")]
        [LookUp("LookUps.Student")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [DisplayName("")]
        public AttendenceViewModel Attendence { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine3 { get; set; }

        public byte?  PresentStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("StartTime")]
        public string StartTimeString { get; set; }

        //public System.TimeSpan?  StartTime { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [CustomDisplay("EndTime")]
        public string  EndTimeString { get; set; }

        //public System.TimeSpan? EndTime { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentAttendenceDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentAttendenceViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var studenDTO = dto as StudentAttendenceDTO;
            Mapper<StudentAttendenceDTO, StudentAttendenceViewModel>.CreateMap();
            var vm = Mapper<StudentAttendenceDTO, StudentAttendenceViewModel>.Map(studenDTO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.StartTimeString = studenDTO.StartTime.HasValue ? DateTime.Today.Add(studenDTO.StartTime.Value).ToString("hh:mm tt") : null;
            vm.EndTimeString = studenDTO.EndTime.HasValue ? DateTime.Today.Add(studenDTO.EndTime.Value).ToString("hh:mm tt") : null;
            vm.Attendence.PresentStatus = studenDTO.PresentStatusID.ToString();
            vm.Student = new KeyValueViewModel() { Key = studenDTO.StudentID.ToString(), Value = studenDTO.StudentName };
            vm.Section = new KeyValueViewModel() { Key = studenDTO.SectionID.ToString(), Value = studenDTO.SectionName };
            vm.StudentClass = new KeyValueViewModel() { Key = studenDTO.ClassID.ToString(), Value = studenDTO.ClassName };
            vm.Attendence.AttendenceDateString = studenDTO.AttendenceDate.HasValue ? studenDTO.AttendenceDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Attendence.Notes = studenDTO.Reason;
            vm.Attendence.AttendenceReasonID = studenDTO.AttendenceReasonID;
            vm.Attendence.AttendenceReason = studenDTO.AttendenceReasonID.HasValue ? studenDTO.AttendenceReasonID.Value.ToString() : null;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentAttendenceViewModel, StudentAttendenceDTO>.CreateMap();
            var dto = Mapper<StudentAttendenceViewModel, StudentAttendenceDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.StartTime = this == null || this.StartTimeString == null || this.StartTimeString == "" ? (TimeSpan?)null : DateTime.Parse(this.StartTimeString).TimeOfDay;
            dto.EndTime = this == null || this.EndTimeString == null || this.EndTimeString == "" ? (TimeSpan?)null : DateTime.Parse(this.EndTimeString).TimeOfDay;
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.SectionID = this.Section == null || string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.ClassID = this.StudentClass == null || string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.PresentStatusID = string.IsNullOrEmpty(this.Attendence.PresentStatus) ? (byte?)null : byte.Parse(this.Attendence.PresentStatus);
            dto.AttendenceDate = string.IsNullOrEmpty(this.Attendence.AttendenceDateString) ? (DateTime?)null : DateTime.ParseExact(Attendence.AttendenceDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.Reason = this.Attendence.Notes;
            dto.AttendenceReasonID = this.Attendence.AttendenceReasonID;
            dto.AttendenceReasonID = string.IsNullOrEmpty(this.Attendence.AttendenceReason) ? (byte?) null : byte.Parse(this.Attendence.AttendenceReason);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentAttendenceDTO>(jsonString);
        }
    }
}

