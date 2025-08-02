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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MCGridInventoryDocumentType", "gridModel.MCGridInventoryDocumentType")]
    [DisplayName("Invoice Document Type")]
    public class MCGridInventoryDocumentTypeViewModel : BaseMasterViewModel
    {
      
      
        public int? DocumentTypeID { get; set; } //Level 1

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Document Type")]
        public string DocumentTypeName { get; set; }


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
        [CustomDisplay("Diff.")]
        public decimal? DifferenceAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Diff(%)+KPI")]
        public decimal? DifferencePlusKPI { get; set; }
    }
}
