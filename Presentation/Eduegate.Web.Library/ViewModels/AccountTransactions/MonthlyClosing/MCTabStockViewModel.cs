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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabStock", "CRUDModel.ViewModel.MCTabStock")]
    [DisplayName("Stock")]
    public class MCTabStockViewModel : BaseMasterViewModel
    {
        public MCTabStockViewModel()
        {
            MCGridStockType = new List<MCGridStockTypeViewModel>() { new MCGridStockTypeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewStockType()")]
        [CustomDisplay("Fill Data")]
        public string ViewStockTypeButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Stock")]
        public List<MCGridStockTypeViewModel> MCGridStockType { get; set; }


    }
}