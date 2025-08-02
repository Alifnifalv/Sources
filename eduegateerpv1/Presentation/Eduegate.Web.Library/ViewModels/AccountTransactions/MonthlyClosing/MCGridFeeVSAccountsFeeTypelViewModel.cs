using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MCGridFeeVSAccountsFeeType", "CRUDModel.ViewModel.MCTabFeeVSAccounts.MCGridFeeVSAccountsFeeType")]
    [DisplayName("Fee Type")]
    public class MCGridFeeVSAccountsFeeTypelViewModel : BaseMasterViewModel
    {
        public MCGridFeeVSAccountsFeeTypelViewModel()
        {          
            MCGridFeeVSAccountsFeeCycle = new List<MCGridFeeVSAccountsFeeCycleViewModel>() { new MCGridFeeVSAccountsFeeCycleViewModel() };
        }


        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "medium - col - width textleft")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; } = false;
        public int? FeeTypeID { get; set; } //Level 1

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("FeeType")]
        public string FeeTypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Fee Amount")]
        public decimal? FeeAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Account Amount")]
        public decimal? AccountAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Difference")]
        public decimal? TransactionCredit { get; set; }
       

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]// AccountAmount -FeeAmount
        [CustomDisplay("Diff.")]
        public decimal? DifferenceAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Diff(%)+KPI")]
        public decimal? DifferencePlusKPI { get; set; }


        [ControlType(Framework.Enums.ControlTypes.GridGroup, "MCGridFeeVSAccountsFeeCycle", Attributes4 = "colspan=9")]
        [DisplayName("")]
        public List<MCGridFeeVSAccountsFeeCycleViewModel> MCGridFeeVSAccountsFeeCycle { get; set; }
    }
}
