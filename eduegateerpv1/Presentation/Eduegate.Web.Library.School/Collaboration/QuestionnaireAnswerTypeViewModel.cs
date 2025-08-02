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
    public class QuestionnaireAnswerTypeViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("QuestionnaireAnswerTypeID")]
        public int  QuestionnaireAnswerTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("TypeName")]
        public string  TypeName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as QuestionnaireAnswerTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireAnswerTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<QuestionnaireAnswerTypeDTO, QuestionnaireAnswerTypeViewModel>.CreateMap();
            var vm = Mapper<QuestionnaireAnswerTypeDTO, QuestionnaireAnswerTypeViewModel>.Map(dto as QuestionnaireAnswerTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<QuestionnaireAnswerTypeViewModel, QuestionnaireAnswerTypeDTO>.CreateMap();
            var dto = Mapper<QuestionnaireAnswerTypeViewModel, QuestionnaireAnswerTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireAnswerTypeDTO>(jsonString);
        }
    }
}

