using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Settings;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Settings
{
    public class SequenceViewModel: BaseMasterViewModel
    {
        ///[Required]
        ///[ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("SequenceID")]
        public int SequenceID { get; set; }
        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("SequenceType")]
        public string SequenceType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Prefix")]
        public string Prefix { get; set; }
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SequenceDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SequenceViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SequenceDTO, SequenceViewModel>.CreateMap();
            var vm = Mapper<SequenceDTO, SequenceViewModel>.Map(dto as SequenceDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SequenceViewModel, SequenceDTO>.CreateMap();
            var dto = Mapper<SequenceViewModel, SequenceDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SequenceDTO>(jsonString);
        }
    }
}
