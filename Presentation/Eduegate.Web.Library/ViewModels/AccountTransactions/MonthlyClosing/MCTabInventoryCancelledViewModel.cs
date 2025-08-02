using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabInventoryCancelled", "CRUDModel.ViewModel.MCTabInventoryCancelled")]
    [DisplayName("Inventory Cancelled")]
    public class MCTabInventoryCancelledViewModel : BaseMasterViewModel
    {
        public MCTabInventoryCancelledViewModel()
        {
            MCGridInventoryCancel = new List<MCGridInventoryCancelViewModel>() { new MCGridInventoryCancelViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewInventoryCancel()")]
        [CustomDisplay("Fill Data")]
        public string ViewInventoryCancelButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Inventory Cancelled")]
        public List<MCGridInventoryCancelViewModel> MCGridInventoryCancel { get; set; }
    }
}
