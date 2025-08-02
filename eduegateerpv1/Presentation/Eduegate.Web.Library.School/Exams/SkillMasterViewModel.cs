using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    public class SkillMasterViewModel : BaseMasterViewModel
    {
        public int SkillMasterID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        
        [Required]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("SkillName")]
        public string SkillName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Skill", "Numeric", false)]
        [LookUp("LookUps.Skills")]
        [CustomDisplay("SkillGroup")]
        public KeyValueViewModel SkillGroup { get; set; }
        public int? SkillGroupMasterID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SkillMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SkillMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SkillMasterDTO, SkillMasterViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var vm = Mapper<SkillMasterDTO, SkillMasterViewModel>.Map(dto as SkillMasterDTO);
            var sDto = dto as SkillMasterDTO;
            vm.SkillGroup = sDto.SkillGroupMasterID.HasValue ? new KeyValueViewModel() { Key = sDto.SkillGroup.Key, Value = sDto.SkillGroup.Value } : new KeyValueViewModel();

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SkillMasterViewModel, SkillMasterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SkillMasterViewModel, SkillMasterDTO>.Map(this);
            dto.SkillGroupMasterID = string.IsNullOrEmpty(this.SkillGroup.Key) ? (int?)null : int.Parse(this.SkillGroup.Key);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SkillMasterDTO>(jsonString);
        }
    }
}
