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
    public class AccountEntryViewModel : BaseMasterViewModel
    {
        public AccountEntryViewModel()
        {
            Group = new KeyValueViewModel();
            IsEnableSubLedger = false;
            MainGroup = new KeyValueViewModel();
            SubGroup = new KeyValueViewModel();
        }

        public long AccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("MainGroup")]
        [Select2("MainGroup", "Numeric", false, "MainGroupChanges($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.MainGroup")]
        public KeyValueViewModel MainGroup { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("SubGroup")]
        [Select2("SubGroup", "Numeric", false, "SubGroupChanges($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.SubGroup")]
        public KeyValueViewModel SubGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("AccountGroups")]
        [Select2("AccountGroups", "Numeric", false, "AccountGroupChanges($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.AccountGroup")]
        [QuickSmartView("AccountGroups")]
        public KeyValueViewModel Group { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AccountCode")]
        [MaxLength(30, ErrorMessage = "Maximum Length should be within 30!")]
        public string AccountCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("AccountName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string AccountName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Alias")]
        [MaxLength(30, ErrorMessage = "Maximum Length should be within 30!")]
        public string Alias { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Behaviour")]
        [LookUp("LookUps.AccountBehavior")]
        public string Behavior { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsEnableSubLedger")]
        public bool? IsEnableSubLedger { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AccountsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountEntryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AccountsDTO, AccountEntryViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var agDto = dto as AccountsDTO;
            var vm = Mapper<AccountsDTO, AccountEntryViewModel>.Map(dto as AccountsDTO);
            vm.AccountID = agDto.AccountID;
            vm.AccountCode = agDto.AccountCode;
            vm.Alias = agDto.Alias;
            vm.AccountName = agDto.AccountName;
            vm.Group = new KeyValueViewModel() { Key = agDto.AccountGroup.Key, Value = agDto.AccountGroup.Value };
            //vm.Behavior = ((int)agDto.AccountBehavior).ToString();
            vm.Behavior = agDto.AccountBehavoirID.ToString();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AccountEntryViewModel, AccountsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<AccountEntryViewModel, AccountsDTO>.Map(this);
            dto.AccountID = this.AccountID;
            dto.AccountCode = this.AccountCode;
            dto.Alias = this.Alias;
            dto.AccountName = this.AccountName;
            dto.GroupID = this.Group == null || string.IsNullOrEmpty(this.Group.Key) ? (int?)null : int.Parse(this.Group.Key);
            dto.AccountBehavoirID = string.IsNullOrEmpty(this.Behavior) ? (byte?)null : byte.Parse(this.Behavior);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountsDTO>(jsonString);
        }
    }
}
