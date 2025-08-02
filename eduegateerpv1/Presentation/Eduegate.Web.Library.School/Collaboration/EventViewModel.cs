using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Collaboration
{
    public class EventViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("EventIID")]
        public long  EventIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("EventTitle")]
        public string  EventTitle { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Description")]
        public string  Description { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("StartDate")]
        public System.DateTime?  StartDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("EndDate")]
        public System.DateTime?  EndDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("IsThisAHoliday")]
        public bool?  IsThisAHoliday { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("IsEnableReminders")]
        public bool?  IsEnableReminders { get; set; }
        
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EventDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EventViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EventDTO, EventViewModel>.CreateMap();
            var vm = Mapper<EventDTO, EventViewModel>.Map(dto as EventDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EventViewModel, EventDTO>.CreateMap();
            var dto = Mapper<EventViewModel, EventDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EventDTO>(jsonString);
        }
    }
}

