using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Eduegate.Web.Library.School.Fees
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeInvoice", "CRUDModel.ViewModel.FeeInvoice")]
    [DisplayName("FeeInvoice")]
    public class FeeCollectionPendingInvoiceViewModel : BaseMasterViewModel
    {
        public FeeCollectionPendingInvoiceViewModel()
        {
            
            IsRowSelected = false;
         

        }

        //[ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        //[DisplayName(" ")]
        //public bool IsExpand { get; set; }

        public bool? IsExternal { get; set; }

        public string ReportName { get; set; }

        public bool? IsRowCheckBoxDisable { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2",attribs: "ng-change=SelectFees(gridModel) ng-disabled=gridModel.IsRowCheckBoxDisable")]
        [CustomDisplay("Selection")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft2")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceNo")]
        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceDate")]
        public string InvoiceDate { get; set; }
       

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceAmount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Credit/DebitNote")]
        public decimal? CrDrAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("CollectedAmount")]
        public decimal? CollAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("Balance")]
        public decimal? Balance { get; set; }

        public long? StudentFeeDueID { get; set; }
       
        public int? FeePeriodID { get; set; }

        public int? FeeMasterID { get; set; }

    }
}
