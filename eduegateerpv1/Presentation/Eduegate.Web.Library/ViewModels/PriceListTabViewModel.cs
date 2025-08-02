using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    public class PriceListTabViewModel : BaseMasterViewModel
    {
        public PriceListTabViewModel()
        {
            PriceLists = new List<BranchPriceListMapViewModel>() { new BranchPriceListMapViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("")]
        public List<BranchPriceListMapViewModel> PriceLists { get; set; }
    }
}
