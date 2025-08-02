using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Enums.Accounting;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class AccountViewModel : BaseMasterViewModel
    {
        public AccountViewModel()
        {
            //Parent = new KeyValueViewModel();
            Group = new KeyValueViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Account ID")]
        public long AccountID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Account Code")]
        [MaxLength(30, ErrorMessage = "Maximum Length should be within 30!")]

        public string AccountCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Alias")]
        [MaxLength(30, ErrorMessage = "Maximum Length should be within 30!")]
        public string Alias { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Account Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 30!")]
        public string AccountName { get; set; }
        public AccountViewModel ParentAccount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Parent Account")]
        //[Select2("Accounts", "Numeric", false)]
        //[LookUp("LookUps.Accounts")]
        //[QuickSmartView("Accounts")]
        //public KeyValueViewModel Parent { get; set; }

        //public AccountGroupViewModel AccountGroup { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Account Groups")]
        [Select2("AccountGroups", "Numeric", false)]
        [LookUp("LookUps.AccountGroup")]
        [QuickSmartView("AccountGroups")]
        public KeyValueViewModel Group { get; set; }
        public AccountBehavior AccountBehavior { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Behaviour")]
        [LookUp("LookUps.AccountBehavior")]
        public string Behavior { get; set; }

        public static AccountViewModel FromDTO(AccountDTO dto)
        {
            Mapper<AccountDTO, AccountViewModel>.CreateMap();
            Mapper<AccountGroupDTO, AccountGroupViewModel>.CreateMap();
            var mapper = Mapper<AccountDTO, AccountViewModel>.Map(dto);
            mapper.Group = new KeyValueViewModel() { Key = dto.AccountGroup.AccountGroupID.ToString(), Value = dto.AccountGroup.GroupName };
            mapper.Behavior = ((int)dto.AccountBehavior).ToString();
            //mapper.Parent = dto.ParentAccount == null ? new KeyValueViewModel() : new KeyValueViewModel() { Key = dto.ParentAccount.AccountID.ToString(), Value = dto.ParentAccount.AccountName  };
            return mapper;
        }
        public static AccountDTO ToDTO(AccountViewModel vm)
        {
            Mapper<AccountViewModel, AccountDTO>.CreateMap();
            Mapper<AccountGroupViewModel, AccountGroupDTO>.CreateMap();
            var mapper = Mapper<AccountViewModel, AccountDTO>.Map(vm);
            mapper.AccountGroup = new AccountGroupDTO() { AccountGroupID = int.Parse(vm.Group.Key) };
            mapper.AccountBehavior = (AccountBehavior)Enum.Parse(typeof(AccountBehavior), vm.Behavior);
            //mapper.ParentAccount = !string.IsNullOrEmpty(vm.Parent.Value) ? new AccountDTO() { AccountID = long.Parse(vm.Parent.Key)} : null;
            return mapper;
        }
    }
}
