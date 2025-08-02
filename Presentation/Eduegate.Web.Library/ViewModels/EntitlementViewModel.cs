using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerEntitlements", "CRUDModel.ViewModel.Entitlements")]
    [DisplayName("Entitlements")]
    public partial class EntitlementViewModel : BaseMasterViewModel
    {
        public EntitlementViewModel()
        {
            EntitlementMaps = new List<EntitlementMapViewModel>() { new EntitlementMapViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Entitlements")]
        public List<EntitlementMapViewModel> EntitlementMaps { get; set; }
    }
}
