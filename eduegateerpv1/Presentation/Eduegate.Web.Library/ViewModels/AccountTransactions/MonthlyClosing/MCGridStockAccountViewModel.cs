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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MCGridStockAccount", "gridModel.MCGridStockAccount")]
    [DisplayName("Stock Account")]
    public  class MCGridStockAccountViewModel : BaseMasterViewModel
    {


        public long? AccountID { get; set; } //Level 1

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Account Name")]
        public string AccountName { get; set; }
        public decimal? OpeningDebit { get; set; }
        public decimal? OpeningCredit { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("OpeningAmount")]
        public decimal? OpeningAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Transaction Debit")]
        public decimal? TransactionDebit { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Transaction Credit")]
        public decimal? TransactionCredit { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        //[CustomDisplay("TransactionAmount")]
        public decimal? TransactionAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        //[CustomDisplay("ClosingDebit")]
        public decimal? ClosingDebit { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        //[CustomDisplay("ClosingCredit")]
        public decimal? ClosingCredit { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("ClosingAmount")]
        public decimal? ClosingAmount { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]// Closing -Opening
        [CustomDisplay("Diff.")]
        public decimal? DifferenceAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Diff(%)+KPI")]
        public decimal? DifferencePlusKPI { get; set; }
    }
}
