using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "4", "CRUDModel.ViewModel.ActionTab.Arrangement")]
    [DisplayName("Arrangement/Late Delivery")]
    public class ArrangementViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.Notify")]
        [Select2("Notify", "Numeric", true)]
        [DisplayName("Notify")]
        public List<KeyValueViewModel> Notify { get; set; }

        public long TicketActionDetailDetailMapIID { get; set; }

    }
}
