using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    public class PriceListViewModel : BaseMasterViewModel
    {
        public PriceListViewModel() 
        {
            PriceLists = new List<BranchPriceListViewModel>() { new BranchPriceListViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Price List Map")]
        public List<BranchPriceListViewModel> PriceLists { get; set; }
    }
}
