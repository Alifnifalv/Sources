using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "2", "CRUDModel.ViewModel.ActionTab.CollectItem")]
    [DisplayName("Collect Item")]
    public class CollectItemViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Give item to")]
        [Select2("GiveItemTo", "Numeric", true)]
        [LookUp("LookUps.GiveItemToCollectItem")]
        public KeyValueViewModel GiveItemTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
    }
}
