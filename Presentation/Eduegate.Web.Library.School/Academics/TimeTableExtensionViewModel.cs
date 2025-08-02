using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TimeTableExtension", "CRUDModel.ViewModel")]
    public class TimeTableExtensionViewModel : BaseMasterViewModel
    {
        public TimeTableExtensionViewModel()
        {
            //Class = new KeyValueViewModel();
            //Section = new KeyValueViewModel();
            //OtherTeacher = new List<KeyValueViewModel>();
            //AssociateTeacher = new List<KeyValueViewModel>();
            //MapDetails = new List<TimeTableExtensionMapViewModel>() { new TimeTableExtensionMapViewModel() };
            MapDetails = new TimeTableExtensionMapViewModel();
            IsActive = false;
            IsPeriodContinues = false;
        }

        // [Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Id")]
        public long TimeTableExtIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("TimeTable")]
        [LookUp("LookUps.TimeTable")]

        public string TimeTable { get; set; }
        public int? TimeTableID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Name")]
        public string TimeTableExtName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("SubjectType", "String", false, "SubjectTypeChanges($event, $element,CRUDModel.ViewModel)")]
        [CustomDisplay("SubjectType")]
        [LookUp("LookUps.SubjectType")]
        public KeyValueViewModel SubjectType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TotalPeriod/Week")]
        public string PeriodCountWeek { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MinimumPeriod/Day")]
        public string MinPeriodCountDay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaximumPeriod/Day")]
        public string MaxPeriodCountDay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsContinuesPeriod")]
        public bool? IsPeriodContinues { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [CustomDisplay("Details")]
        public TimeTableExtensionMapViewModel MapDetails { get; set; }

        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TimeTableExtensionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TimeTableExtensionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TimeTableExtensionDTO, TimeTableExtensionViewModel>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapDTO = dto as TimeTableExtensionDTO;
            var vm = Mapper<TimeTableExtensionDTO, TimeTableExtensionViewModel>.Map(mapDTO);
            var ClsTcrDto = dto as ClassTeacherMapDTO;

            vm.TimeTableExtIID = mapDTO.TimeTableExtIID;
            vm.TimeTableExtName = mapDTO.TimeTableExtName;
            vm.SubjectType = mapDTO.SubjectTypeID.HasValue ? new KeyValueViewModel() { Key = mapDTO.SubjectTypeID.ToString(), Value = mapDTO.SubjectTypeName } : new KeyValueViewModel();
            vm.TimeTable = mapDTO.TimeTableID.IsNotNull() ? mapDTO.TimeTableID.ToString() : null;
            vm.PeriodCountWeek = mapDTO.PeriodCountWeek;
            vm.MaxPeriodCountDay = mapDTO.MaxPeriodCountDay;
            vm.MinPeriodCountDay = mapDTO.MinPeriodCountDay;
            vm.IsActive = mapDTO.IsActive;
            vm.IsPeriodContinues = mapDTO.IsPeriodContinues;

            vm.SchoolID = mapDTO.SchoolID;
            vm.AcademicYearID = mapDTO.AcademicYearID;

            vm.MapDetails.Class = new List<KeyValueViewModel>();

            foreach (var map in mapDTO.Class)
            {
                vm.MapDetails.Class.Add(new KeyValueViewModel()
                {
                    Key = map.Key,
                    Value = map.Value,
                });
            }

            vm.MapDetails.Section = new List<KeyValueViewModel>();

            foreach (var map in mapDTO.Section)
            {
                vm.MapDetails.Section.Add(new KeyValueViewModel()
                {
                    Key = map.Key,
                    Value = map.Value,
                });
            }

            vm.MapDetails.Subject = new List<KeyValueViewModel>();

            foreach (var map in mapDTO.Subject)
            {
                vm.MapDetails.Subject.Add(new KeyValueViewModel()
                {
                    Key = map.Key,
                    Value = map.Value,
                });
            }

            vm.MapDetails.Teacher = new List<KeyValueViewModel>();

            foreach (var map in mapDTO.Teacher)
            {
                vm.MapDetails.Teacher.Add(new KeyValueViewModel()
                {
                    Key = map.Key,
                    Value = map.Value,
                });
            }

            vm.MapDetails.WeekDay = new List<KeyValueViewModel>();

            foreach (var map in mapDTO.WeekDay)
            {
                vm.MapDetails.WeekDay.Add(new KeyValueViewModel()
                {
                    Key = map.Key,
                    Value = map.Value,
                });
            }

            vm.MapDetails.WeekDayOperator = mapDTO.WeekDayOperator.Key.IsNotNull() ? new KeyValueViewModel() { Key = mapDTO.WeekDayOperator.Key.ToString(), Value = mapDTO.WeekDayOperator.Value } : new KeyValueViewModel();

            vm.MapDetails.ClassTiming = new List<KeyValueViewModel>();

            foreach (var map in mapDTO.ClassTiming)
            {
                vm.MapDetails.ClassTiming.Add(new KeyValueViewModel()
                {
                    Key = map.Key,
                    Value = map.Value,
                });
            }

            vm.MapDetails.ClassTimingOperator = mapDTO.ClassTimingOperator.Key.IsNotNull() ? new KeyValueViewModel() { Key = mapDTO.ClassTimingOperator.Key.ToString(), Value = mapDTO.ClassTimingOperator.Value } : new KeyValueViewModel();

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TimeTableExtensionViewModel, TimeTableExtensionDTO>.CreateMap();
            var dto = Mapper<TimeTableExtensionViewModel, TimeTableExtensionDTO>.Map(this);
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            dto.TimeTableExtIID = this.TimeTableExtIID;
            dto.TimeTableExtName = this.TimeTableExtName;
            dto.TimeTableID = int.Parse(this.TimeTable);
            dto.SubjectTypeID = string.IsNullOrEmpty(this.SubjectType.Key) ? (byte?)null : byte.Parse(this.SubjectType.Key);
            dto.PeriodCountWeek = this.PeriodCountWeek;
            dto.MaxPeriodCountDay = this.MaxPeriodCountDay;
            dto.MinPeriodCountDay = this.MinPeriodCountDay;
            dto.IsActive = this.IsActive;
            dto.IsPeriodContinues = this.IsPeriodContinues;

            dto.SchoolID = this.SchoolID;
            dto.AcademicYearID = this.AcademicYearID;

            dto.Class = new List<KeyValueDTO>();

            foreach (var map in this.MapDetails.Class)
            {
                if (map.Key.IsNotNull())
                {
                    dto.Class.Add(new KeyValueDTO()
                    {
                        Key = map.Key,
                    });
                }
            }

            dto.Section = new List<KeyValueDTO>();

            foreach (var map in this.MapDetails.Section)
            {
                if (map.Key.IsNotNull())
                {
                    dto.Section.Add(new KeyValueDTO()
                    {
                        Key = map.Key,
                    });
                }
            }

            dto.Subject = new List<KeyValueDTO>();

            foreach (var map in this.MapDetails.Subject)
            {
                if (map.Key.IsNotNull())
                {
                    dto.Subject.Add(new KeyValueDTO()
                    {
                        Key = map.Key,
                    });
                }
            }

            dto.Teacher = new List<KeyValueDTO>();

            foreach (var map in this.MapDetails.Teacher)
            {
                if (map.Key.IsNotNull())
                {
                    dto.Teacher.Add(new KeyValueDTO()
                    {
                        Key = map.Key,
                    });
                }
            }

            dto.WeekDay = new List<KeyValueDTO>();

            foreach (var map in this.MapDetails.WeekDay)
            {
                if (map.Key.IsNotNull())
                {
                    dto.WeekDay.Add(new KeyValueDTO()
                    {
                        Key = map.Key,
                    });
                }
            }

            if (this.MapDetails.WeekDayOperator.IsNotNull())
            {
                dto.WeekDayOperator = new KeyValueDTO()
                {
                    Key = this.MapDetails.WeekDayOperator.Key,
                };
            }

            dto.ClassTiming = new List<KeyValueDTO>();

            foreach (var map in this.MapDetails.ClassTiming)
            {
                if (map.Key.IsNotNull())
                {
                    dto.ClassTiming.Add(new KeyValueDTO()
                    {
                        Key = map.Key,
                    });
                }
            }

            if (this.MapDetails.ClassTimingOperator.IsNotNull())
            {
                dto.ClassTimingOperator = new KeyValueDTO()
                {
                    Key = this.MapDetails.ClassTimingOperator.Key,
                };
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TimeTableExtensionDTO>(jsonString);
        }
    }
}