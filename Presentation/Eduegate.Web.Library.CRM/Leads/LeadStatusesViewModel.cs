using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Web.Library.Common;
using System.Globalization;

namespace Eduegate.Web.Library.CRM.Leads
{
    public class LeadStatusesViewModel : BaseMasterViewModel
    {
        public byte LeadStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Description")]

        public string LeadStatusName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeadStatusesDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeadStatusesViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeadStatusesDTO, LeadStatusesViewModel>.CreateMap();
            var feeDto = dto as LeadStatusesDTO;
            var vm = Mapper<LeadStatusesDTO, LeadStatusesViewModel>.Map(feeDto);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeadStatusesViewModel, LeadStatusesDTO>.CreateMap();
            var dto = Mapper<LeadStatusesViewModel, LeadStatusesDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeadStatusesDTO>(jsonString);
        }

    }
}

