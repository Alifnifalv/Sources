using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "3", "CRUDModel.ViewModel.ActionTab.DirectReplacement")]
    [DisplayName("Direct Replacement")]
    public class DirectReplacementViewModel :  BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Give item to")]
        [Select2("GiveItemTo", "Numeric", true)]
        [LookUp("LookUps.GiveItemToDirectReplacement")]
        public KeyValueViewModel GiveItemTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
    }
}
