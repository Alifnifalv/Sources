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

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class AccountGroupViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Group ID")]
        public long AccountGroupID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Group Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string GroupName { get; set; }

        public static AccountGroupViewModel FromDTO(AccountGroupDTO dto)
        {
            Mapper<AccountGroupDTO, AccountGroupViewModel>.CreateMap();
            var mapper = Mapper<AccountGroupDTO, AccountGroupViewModel>.Map(dto);
            return mapper;
        }

        public static AccountGroupDTO ToDTO(AccountGroupViewModel vm)
        {
            Mapper<AccountGroupViewModel, AccountGroupDTO>.CreateMap();
            var mapper = Mapper<AccountGroupViewModel, AccountGroupDTO>.Map(vm);
            return mapper;
        }
    }
}
