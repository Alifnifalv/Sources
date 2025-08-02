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
    public class QuestionnaireAnswerViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("QuestionnaireAnswerIID")]
        public long  QuestionnaireAnswerIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Questionnaire Answer Type")]
        [LookUp("LookUps.AnswerType")]
        public string TypeName { get; set; }
        public int?  QuestionnaireAnswerTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Questionnaire")]
        [LookUp("LookUps.Questionnaire")]
        public string Description { get; set; }
        public long?  QuestionnaireID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Answer")]
        public string  Answer { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("MoreInfo")]
        public string  MoreInfo { get; set; }
             
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as QuestionnaireAnswerDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireAnswerViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<QuestionnaireAnswerDTO, QuestionnaireAnswerViewModel>.CreateMap();
            var vm = Mapper<QuestionnaireAnswerDTO, QuestionnaireAnswerViewModel>.Map(dto as QuestionnaireAnswerDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<QuestionnaireAnswerViewModel, QuestionnaireAnswerDTO>.CreateMap();
            var dto = Mapper<QuestionnaireAnswerViewModel, QuestionnaireAnswerDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireAnswerDTO>(jsonString);
        }
    }
}

