using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Exams
{
    public class SkillGroupMasterViewmodel : BaseMasterViewModel
    {
      ///  [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Hidden)]
      ///  [DisplayName("Skill Group Master ID")]
        public int SkillGroupMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("SkillGroupName")]
        public string SkillGroup { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SkillGroupMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SkillGroupMasterViewmodel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SkillGroupMasterDTO, SkillGroupMasterViewmodel>.CreateMap();
            var vm = Mapper<SkillGroupMasterDTO, SkillGroupMasterViewmodel>.Map(dto as SkillGroupMasterDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SkillGroupMasterViewmodel, SkillGroupMasterDTO>.CreateMap();
            var dto = Mapper<SkillGroupMasterViewmodel, SkillGroupMasterDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SkillGroupMasterDTO>(jsonString);
        }
    }
}
