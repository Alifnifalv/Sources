using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabInventory", "CRUDModel.ViewModel.MCTabInventory")]
    [DisplayName("inventory")]
    public class MCTabInventoryViewModel : BaseMasterViewModel
    {

        public MCTabInventoryViewModel()
        {
            MCGridInventoryTransType = new List<MCGridInventoryTransTypeViewModel>() { new MCGridInventoryTransTypeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Inventory")]
        public List<MCGridInventoryTransTypeViewModel> MCGridInventoryTransType { get; set; }
    }
}