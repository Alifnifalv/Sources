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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabAccountsGeneral", "CRUDModel.ViewModel.MCTabAccountsGeneral")]
    [DisplayName("Accounts General")]
    public class MCTabAccountsGeneralViewModel : BaseMasterViewModel
    {
        public MCTabAccountsGeneralViewModel()
        {
            MCGridAccountGeneralMainGroup = new List<MCGridAccountGeneralMainGroupViewModel>() { new MCGridAccountGeneralMainGroupViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewAccountGeneral()")]
        [CustomDisplay("Fill Data")]
        public string ViewAccountGeneralButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Accounts General")]      
        public List<MCGridAccountGeneralMainGroupViewModel> MCGridAccountGeneralMainGroup { get; set; }
    }
}
