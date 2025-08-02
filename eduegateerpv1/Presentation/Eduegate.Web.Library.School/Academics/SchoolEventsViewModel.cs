using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Eduegate.Web.Library.Common;
using System;
using System.Globalization;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SchoolEvents", "CRUDModel.ViewModel")]
    [DisplayName("School Events")]
    public class SchoolEventsViewModel : BaseMasterViewModel
    {
        public SchoolEventsViewModel()
        {

        }

        public long SchoolEventIID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Event Name")]
        public string EventName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Event Date")]
        public string EventDateString { get; set; }
        public DateTime? EventDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Destination")]
        public string Destination { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("Start Time")]
        public string StartTimeString { get; set; }
        //public System.TimeSpan? StartTime { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("End Time")]
        public string EndTimeString { get; set; }
        //public System.TimeSpan? EndTime { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SchoolEventsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SchoolEventsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {

            Mapper<SchoolEventsDTO, SchoolEventsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var sDto = dto as SchoolEventsDTO;
            var vm = Mapper<SchoolEventsDTO, SchoolEventsViewModel>.Map(sDto);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.SchoolEventIID = sDto.SchoolEventIID;
            vm.StartTimeString = sDto.StartTime.HasValue ? DateTime.Today.Add(sDto.StartTime.Value).ToString("hh:mm tt") : null;
            vm.EndTimeString = sDto.EndTime.HasValue ? DateTime.Today.Add(sDto.EndTime.Value).ToString("hh:mm tt") : null;
            vm.EventDateString = sDto.EventDate.HasValue ? sDto.EventDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.AcademicYearID = sDto.AcademicYearID;
            vm.EventName = sDto.EventName;
            vm.Description = sDto.Description;
            vm.Destination = sDto.Destination;


            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SchoolEventsViewModel, SchoolEventsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SchoolEventsViewModel, SchoolEventsDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.SchoolEventIID = this.SchoolEventIID;
            dto.EventName = this.EventName;
            dto.EventDate = string.IsNullOrEmpty(this.EventDateString) ? (DateTime?)null : DateTime.ParseExact(this.EventDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.Description = this.Description != null ? this.Description : null;
            dto.Destination = this.Destination != null ? this.Destination : null;
            dto.StartTime =  this.StartTimeString == null || this.StartTimeString == "" ? (TimeSpan?)null : Convert.ToDateTime(this.StartTimeString).TimeOfDay;
            dto.EndTime = this.EndTimeString == null || this.EndTimeString == "" ? (TimeSpan?)null : Convert.ToDateTime(this.EndTimeString).TimeOfDay;
            dto.SchoolID = this.SchoolID.HasValue ? this.SchoolID : null;
            dto.AcademicYearID = this.AcademicYearID.HasValue ? this.AcademicYearID : null;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SchoolEventsDTO>(jsonString);
        }
    }
}

