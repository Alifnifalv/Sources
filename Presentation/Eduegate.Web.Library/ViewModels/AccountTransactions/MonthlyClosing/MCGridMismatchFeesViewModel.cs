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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MCGridMismatchFees", "CRUDModel.ViewModel.MCTabMisMatchedFees.MCGridMismatchFees")]
    [DisplayName("Fees")]
    public class MCGridMismatchFeesViewModel : BaseMasterViewModel
    {
      
        public int? FeeTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Type Name")]
        public string FeeTypeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Branch")]
        public string SchoolName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("AdmissionNumber")]
        public string AdmissionNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("StudentName")]
        public string StudentName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("InvoiceNo")]
        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Invoice Date")]
        public string InvoiceDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Fee Name")]
        public string FeeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }
    }
}
