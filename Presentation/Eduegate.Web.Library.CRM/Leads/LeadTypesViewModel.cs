using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Framework.Contracts.Common;
using Newtonsoft.Json;
using Eduegate.Framework.Translator;
using System;
using System.Collections.Generic;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.CRM.Leads
{
    public class LeadTypesViewModel : BaseMasterViewModel
    {
        public byte LeadTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [CustomDisplay("TypeName")]
        public string LeadTypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("LeadSequencePrefix")]
        public string LeadSequencePrefix { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeadTypesDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeadTypesViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeadTypesDTO, LeadTypesViewModel>.CreateMap();
            var feeDto = dto as LeadTypesDTO;
            var vm = Mapper<LeadTypesDTO, LeadTypesViewModel>.Map(feeDto);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeadTypesViewModel, LeadTypesDTO>.CreateMap();
            var dto = Mapper<LeadTypesViewModel, LeadTypesDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeadTypesDTO>(jsonString);
        }

    }
}
