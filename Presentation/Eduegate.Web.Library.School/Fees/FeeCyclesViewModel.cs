using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    public class FeeCyclesViewModel : BaseMasterViewModel
    {
     //   [Required]
      //  [ControlType(Framework.Enums.ControlTypes.Label)]
     //   [DisplayName("Fee Cycle ID")]
        public byte FeeCycleID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("FeeCycle")]

        public string Cycle { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeCyclesDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeCyclesViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeeCyclesDTO, FeeCyclesViewModel>.CreateMap();
            var feeDto = dto as FeeCyclesDTO;
            var vm = Mapper<FeeCyclesDTO, FeeCyclesViewModel>.Map(feeDto);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeCyclesViewModel, FeeCyclesDTO>.CreateMap();
            var dto = Mapper<FeeCyclesViewModel, FeeCyclesDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeCyclesDTO>(jsonString);
        }

    }
}
