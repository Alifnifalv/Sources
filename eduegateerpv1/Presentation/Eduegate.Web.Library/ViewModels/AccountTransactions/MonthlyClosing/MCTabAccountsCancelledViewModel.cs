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
            MCGridFeeDueFeeType = new List<MCGridFeeDueFeeTypeViewModel>() { new MCGridFeeDueFeeTypeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Accounts Cancelled")]
        public List<MCGridFeeDueFeeTypeViewModel> MCGridFeeDueFeeType { get; set; }
    }
}
