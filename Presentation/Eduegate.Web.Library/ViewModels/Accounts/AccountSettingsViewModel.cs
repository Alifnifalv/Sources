using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DocumentType", "CRUDModel.ViewModel")]
    [DisplayName("Account Settings")]
    public class AccountSettingsViewModel: BaseMasterViewModel
    {
        [DataPicker("AccountEntry")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("GLAccountID")]
        public long? GLAccountID { get; set; }
    }
}
