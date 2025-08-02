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
    public class EventAudienceTypeViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("EventAudienceTypeID")]
        public byte  EventAudienceTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("AudienceTypeName")]
        public string  AudienceTypeName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EventAudienceTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EventAudienceTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EventAudienceTypeDTO, EventAudienceTypeViewModel>.CreateMap();
            var vm = Mapper<EventAudienceTypeDTO, EventAudienceTypeViewModel>.Map(dto as EventAudienceTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EventAudienceTypeViewModel, EventAudienceTypeDTO>.CreateMap();
            var dto = Mapper<EventAudienceTypeViewModel, EventAudienceTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EventAudienceTypeDTO>(jsonString);
        }
    }
}

