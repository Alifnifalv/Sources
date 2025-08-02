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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MCGridFeeVSAccountsFeeMaster", "gridModel.MCGridFeeVSAccountsFeeMaster")]
    [DisplayName("Fee master")]
  public  class MCGridFeeVSAccountsFeeMasterViewModel : BaseMasterViewModel
    {
        

        public int? FeeMasterID { get; set; } //Level 3

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Fee")]
        public string FeeMasterName { get; set; }

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

    }
}
