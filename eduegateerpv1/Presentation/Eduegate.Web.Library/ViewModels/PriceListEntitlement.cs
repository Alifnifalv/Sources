using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    public class PriceListEntitlement : BaseMasterViewModel
    {
        public PriceListEntitlement()
        {
            EntitlementPriceListMaps = new List<EntitlementPriceListMapViewModel>() { new EntitlementPriceListMapViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Entitlements")]
        public List<EntitlementPriceListMapViewModel> EntitlementPriceListMaps { get; set; }
    }
}
