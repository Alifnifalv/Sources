using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Security
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SecuritySettings", "CRUDModel.ViewModel.SecuritySettings")]
    [DisplayName("Security")]
    public class SecuritySettingsViewModel : BaseMasterViewModel
    {
        public SecuritySettingsViewModel()
        {
            Claims = new List<SecurityClaimViewModel>() { new SecurityClaimViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.ClaimSets")]
        [CustomDisplay("ClaimSets")]
        [Select2("ClaimSets", "Numeric", true)]
        public List<KeyValueViewModel> ClaimSets { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<SecurityClaimViewModel> Claims { get; set; }
    }
}
