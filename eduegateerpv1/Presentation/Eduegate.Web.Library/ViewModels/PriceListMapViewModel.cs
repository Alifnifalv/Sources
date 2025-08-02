using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Maps", "CRUDModel.ViewModel.PriceListsMap.Maps")]
    [DisplayName("")]
    public class PriceListMapViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Price List")]
        public string PriceListName { get; set; }

        [DataPicker("Price")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Pick")]
        public long ProductPriceListIID { get; set; }
    }
}
