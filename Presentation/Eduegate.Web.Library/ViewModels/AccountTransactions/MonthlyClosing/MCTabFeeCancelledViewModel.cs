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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabFeeCancelled", "CRUDModel.ViewModel.MCTabFeeCancelled")]
    [DisplayName("Fee Cancelled")]
   public class MCTabFeeCancelledViewModel : BaseMasterViewModel
    {
        public MCTabFeeCancelledViewModel()
        {
            MCGridFeeCancelFeeType = new List<MCGridFeeCancelFeeTypeViewModel>() { new MCGridFeeCancelFeeTypeViewModel() };
        }


        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewFeeCancel()")]
        [CustomDisplay("Fill Data")]
        public string ViewFeeCancelButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Fee Cancelled")]
        public List<MCGridFeeCancelFeeTypeViewModel> MCGridFeeCancelFeeType { get; set; }
    }
}