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
    public class QuestionnaireSetViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("QuestionnaireSetID")]
        public int  QuestionnaireSetID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("QuestionnaireSetName")]
        public string  QuestionnaireSetName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as QuestionnaireSetDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireSetViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<QuestionnaireSetDTO, QuestionnaireSetViewModel>.CreateMap();
            var vm = Mapper<QuestionnaireSetDTO, QuestionnaireSetViewModel>.Map(dto as QuestionnaireSetDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<QuestionnaireSetViewModel, QuestionnaireSetDTO>.CreateMap();
            var dto = Mapper<QuestionnaireSetViewModel, QuestionnaireSetDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireSetDTO>(jsonString);
        }
    }
}

