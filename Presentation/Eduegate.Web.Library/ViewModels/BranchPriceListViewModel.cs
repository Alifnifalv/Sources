using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "PriceLists", "CRUDModel.ViewModel.PriceListMap.PriceLists")]
    [DisplayName("Price Lists")]
    public class BranchPriceListViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label,"one-col-")] 
        [DisplayName("Price List ID")]
        public string PriceListID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string PriceDescription { get; set; }

    }
}
