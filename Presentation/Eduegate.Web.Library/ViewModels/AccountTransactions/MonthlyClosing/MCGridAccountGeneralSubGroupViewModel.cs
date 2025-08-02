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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MCGridAccountGeneralSubGroup", "gridModel.MCGridAccountGeneralSubGroup")]
    [DisplayName("Sub Group")]
   public class MCGridAccountGeneralSubGroupViewModel : BaseMasterViewModel
    {
        public MCGridAccountGeneralSubGroupViewModel()
        {
            MCGridAccountGeneralGroup = new List<MCGridAccountGeneralGroupViewModel>() { new MCGridAccountGeneralGroupViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "medium - col - width textleft")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; } = false;
        public long? SubGroupID { get; set; } //Level 2

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Sub Group")]
        public string SubGroupName { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        //[CustomDisplay("OpeningDebit")]
        public decimal? OpeningDebit { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        //[CustomDisplay("OpeningCredit")]
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
        [CustomDisplay("Net Amount")]
        public decimal? DifferenceAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("KPI")]
        public decimal? DifferencePlusKPI { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "MCGridAccountGeneralGroup", Attributes4 = "colspan=10")]
        [DisplayName("")]
        public List<MCGridAccountGeneralGroupViewModel> MCGridAccountGeneralGroup { get; set; }

    }
}
