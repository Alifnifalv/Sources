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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabFeeVSAccounts", "CRUDModel.ViewModel.MCTabFeeVSAccounts")]
    [DisplayName("Fee VS Accounts")]
   public class MCTabFeeVSAccountslViewModel : BaseMasterViewModel
    {

        public MCTabFeeVSAccountslViewModel()
        {
            MCGridFeeVSAccountsFeeType = new List<MCGridFeeVSAccountsFeeTypelViewModel>() { new MCGridFeeVSAccountsFeeTypelViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Fee VS Accounts")]
        public List<MCGridFeeVSAccountsFeeTypelViewModel> MCGridFeeVSAccountsFeeType { get; set; }
    }
}
