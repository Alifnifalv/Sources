using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class MediumViewModel : BaseMasterViewModel
    {
       /// [Required]
        ///[ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("Id")]
        public byte  MediumID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MediumDescription")]
        public string  MediumDescription { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as MediumDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MediumViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<MediumDTO, MediumViewModel>.CreateMap();
            var vm = Mapper<MediumDTO, MediumViewModel>.Map(dto as MediumDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<MediumViewModel, MediumDTO>.CreateMap();
            var dto = Mapper<MediumViewModel, MediumDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<MediumDTO>(jsonString);
        }
    }
}

