using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class AcademicCalendarMasterViewModel : BaseMasterViewModel
    {
        public long AcademicCalendarID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change=AcademicChanges($event)")]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]
        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CalendarName")]
        public string CalendarName { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("CalendarStatus")]
        [LookUp("LookUps.AcademicYearCalendarStatus")]
        public string AcademicCalendarStatus { get; set; }
        public byte? AcademicCalendarStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("CalendarType")]
        [LookUp("LookUps.CalendarType")]
        public string CalendarType { get; set; }
        public byte? CalendarTypeID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AcademicCalendarMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AcademicCalendarMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AcademicCalendarMasterDTO, AcademicCalendarMasterViewModel>.CreateMap();
            var Acdto = dto as AcademicCalendarMasterDTO;
            var vm = Mapper<AcademicCalendarMasterDTO, AcademicCalendarMasterViewModel>.Map(Acdto);

            vm.AcademicCalendarID = Acdto.AcademicCalendarID;
            vm.CalendarName = Acdto.CalenderName;
            vm.Description = Acdto.Description;
            vm.AcademicYear = Acdto.AcademicYearID.ToString();
            vm.AcademicCalendarStatus = Acdto.AcademicCalendarStatusID.ToString();
            vm.CalendarType = Acdto.CalendarTypeID.HasValue ? Acdto.CalendarTypeID.ToString() : null;
            //vm.AcademicYearCalendarEventType = Acdto.AcademicCalendarEventTypeID.ToString();

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AcademicCalendarMasterViewModel, AcademicCalendarMasterDTO>.CreateMap();
            var dto = Mapper<AcademicCalendarMasterViewModel, AcademicCalendarMasterDTO>.Map(this);

            dto.AcademicCalendarID = this.AcademicCalendarID;
            dto.CalenderName = this.CalendarName;
            dto.Description = this.Description;
            dto.AcademicYearID = int.Parse(this.AcademicYear);
            dto.AcademicCalendarStatusID = byte.Parse(this.AcademicCalendarStatus);
            dto.CalendarTypeID = string.IsNullOrEmpty(this.CalendarType) ? (byte?)null : byte.Parse(this.CalendarType);
            //dto.AcademicCalendarEventTypeID = byte.Parse(this.AcademicYearCalendarEventType);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AcademicCalendarMasterDTO>(jsonString);
        }
    }
}

