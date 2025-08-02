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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabAccountsCancelled", "CRUDModel.ViewModel.MCTabAccountsCancelled")]
    [DisplayName("Accounts Cancelled")]
    public class MCTabAccountsCancelledViewModel : BaseMasterViewModel
    {
        public MCTabAccountsCancelledViewModel()
        {
            MCGridAccountCancelFeeType = new List<MCGridAccountCancelTypeViewModel>() { new MCGridAccountCancelTypeViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewAccountsCancel()")]
        [CustomDisplay("Fill Data")]
        public string ViewAccountsCancelButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Accounts Cancelled")]
        public List<MCGridAccountCancelTypeViewModel> MCGridAccountCancelFeeType { get; set; }
    }
}
