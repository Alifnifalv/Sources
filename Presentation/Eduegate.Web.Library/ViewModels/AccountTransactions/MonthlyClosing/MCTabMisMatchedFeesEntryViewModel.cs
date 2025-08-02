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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabMisMatchedFees", "CRUDModel.ViewModel.MCTabMisMatchedFees")]
    [DisplayName("Mismatched Entries of Fees(Amount,Fees,Student,Academic Year,Class,Section etc")]
    public class  MCTabMisMatchedFeesEntryViewModel : BaseMasterViewModel
    {
        public MCTabMisMatchedFeesEntryViewModel()
        {
            MCGridMismatchFees = new List<MCGridMismatchFeesViewModel>() { new MCGridMismatchFeesViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewMismatchFees()")]
        [CustomDisplay("Fill Data")]
        public string ViewMismatchFeesButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Mismatched Entries of Fees")]
        public List<MCGridMismatchFeesViewModel> MCGridMismatchFees { get; set; }
    }
}
