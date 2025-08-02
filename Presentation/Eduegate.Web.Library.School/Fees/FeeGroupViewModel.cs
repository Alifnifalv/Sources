using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;


namespace Eduegate.Web.Library.School.Fees
{
    public class FeeGroupViewModel : BaseMasterViewModel
    {
       /// [Required]
      ///  [ControlType(Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("Fee Group ID")]
        public int  FeeGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("Description")]
        public string  Description { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeeGroupDTO, FeeGroupViewModel>.CreateMap();
            var vm = Mapper<FeeGroupDTO, FeeGroupViewModel>.Map(dto as FeeGroupDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeGroupViewModel, FeeGroupDTO>.CreateMap();
            var dto = Mapper<FeeGroupViewModel, FeeGroupDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeGroupDTO>(jsonString);
        }
    }
}

