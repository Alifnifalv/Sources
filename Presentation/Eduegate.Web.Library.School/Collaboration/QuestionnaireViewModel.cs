using Newtonsoft.Json;
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
    public class QuestionnaireViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("QuestionnaireIID")]
        public long  QuestionnaireIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Description")]
        public string  Description { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Questionnaire Answer Type")]
        [LookUp("LookUps.AnswerType")]
        public string TypeName { get; set; }           
        public int?  QuestionnaireAnswerTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("MoreInfo")]
        public string  MoreInfo { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as QuestionnaireDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<QuestionnaireDTO, QuestionnaireViewModel>.CreateMap();
            var vm = Mapper<QuestionnaireDTO, QuestionnaireViewModel>.Map(dto as QuestionnaireDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<QuestionnaireViewModel, QuestionnaireDTO>.CreateMap();
            var dto = Mapper<QuestionnaireViewModel, QuestionnaireDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuestionnaireDTO>(jsonString);
        }
    }
}

