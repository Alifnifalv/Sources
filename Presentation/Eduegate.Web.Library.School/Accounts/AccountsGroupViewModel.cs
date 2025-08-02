using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Accounts;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.School.Accounts
{
    public class AccountsGroupViewModel : BaseMasterViewModel
    {
        public AccountsGroupViewModel()
        {
            ParentGroup = new KeyValueViewModel();
            IsAffect = false;
        }

        public int GroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ParentGroup", "Numeric", false, "ParentGroupChanges($event, $element, CRUDModel.ViewModel)", false)]
        [CustomDisplay("ParentGroup")]
        [LookUp("LookUps.ParentGroup")]
        public KeyValueViewModel ParentGroup { get; set; }
        public int? Parent_ID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [CustomDisplay("GroupCode")]
        public string GroupCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("GroupName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string GroupName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsAffectRevenueandExpenditure")]
        public bool IsAffect { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AccountsGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountsGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AccountsGroupDTO, AccountsGroupViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var agDto = dto as AccountsGroupDTO;
            var vm = Mapper<AccountsGroupDTO, AccountsGroupViewModel>.Map(dto as AccountsGroupDTO);
            vm.GroupID = agDto.GroupID;
            vm.GroupName = agDto.GroupName;
            vm.GroupCode = agDto.GroupCode;
            vm.ParentGroup = new KeyValueViewModel()
            {
                Key = agDto.Parent_ID.ToString(),
                Value = agDto.ParentGroup.Value
            };
            vm.IsAffect = Convert.ToBoolean(agDto.Affect_ID);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AccountsGroupViewModel, AccountsGroupDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<AccountsGroupViewModel, AccountsGroupDTO>.Map(this);
            dto.GroupID = this.GroupID;
            dto.GroupName = this.GroupName;
            dto.GroupCode = this.GroupCode;
            dto.Parent_ID = this.ParentGroup == null || string.IsNullOrEmpty(this.ParentGroup.Key) ? (int?)null : int.Parse(this.ParentGroup.Key);
            dto.Affect_ID = Convert.ToInt32(this.IsAffect);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountsGroupDTO>(jsonString);
        }
    }
}