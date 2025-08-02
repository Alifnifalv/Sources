using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeTypes")]
    [DisplayName("FeeTypes")]
    public class FinalSettlementFeeTypeViewModel : BaseMasterViewModel
    {
        public FinalSettlementFeeTypeViewModel()
        {
            IsRowSelected = true;
            IsFeePeriodDisabled = true;           
            FeeDueMonthlyFinal = new List<FeeDueMonthlyFinalViewModel>() { new FeeDueMonthlyFinalViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft2", Attributes = "ng-change=ClearSelection(gridModel) ng-disabled=true")]
        [CustomDisplay("Selection")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceNo")]
        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceDate")]
        public string InvoiceDateString { get; set; }
        public DateTime? InvoiceDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Fee")]
        public string FeeMaster { get; set; }
        public byte? FeeCycleID { get; set; }

        public int? FeeMasterID { get; set; }

        public int? FineMasterID { get; set; }
        public long? FineMasterStudentMapID { get; set; }

        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("FeePeriod")]
        public string FeePeriod { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("FeePeriod", "Numeric", false, "FeePeriodChanges($event, $element, gridModel)", optionalAttribute1: "ng-disabled=gridModel.IsFeePeriodDisabled")]
        //[DisplayName("Fee Periods")]
        //[LookUp("LookUps.FeePeriod")]
        //public KeyValueViewModel FeePeriod { get; set; }

        public int? FeePeriodID { get; set; }

        // [Required]
        

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("FeeDue")]
        public decimal? FeeDueAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("CreditNote")]
        public decimal? CreditNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("PrvCollected")]
        public decimal? CollectedAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes2 = "ng-blur=SplitFeeAmount(gridModel,1) ng-disabled=!gridModel.IsRowSelected")]
        [MaxLength(15)]
        [CustomDisplay("Receivable")]
        public decimal? PayableAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-blur=SplitFeeAmount(gridModel,2) ng-disabled=!gridModel.IsRowSelected")]
        [MaxLength(15)]
        [CustomDisplay("Refund")]
        public decimal? Refund { get; set; }

     

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", attribs: "ng-disabled=true")]
        [MaxLength(15)]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        public bool? IsRefundable { get; set; }

        public bool? FeeCollectionStatus { get; set; }

        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
       

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes4 = "colspan=10")]
        [DisplayName("")]
        public List<FeeDueMonthlyFinalViewModel> FeeDueMonthlyFinal { get; set; }
    }
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeDueMonthlyFinal", "gridModel.FeeDueMonthlyFinal",
       gridBindingPrefix: "FeeDueMonthlyFinal")]
    [DisplayName("Split")]
    public class FeeDueMonthlyFinalViewModel : BaseMasterViewModel
    {
        public long MapIID { get; set; }

        public long? FeeStructureMontlySplitMapID { get; set; }
        public long?  FeeDueMonthlySplitID { get; set; }
        public long? ParentID { get; set; }

        public int MonthID { get; set; }
        public int Year { get; set; }
        public int? FeePeriodID { get; set; }
       
        public bool? FeeCollectionStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", Attributes = "ng-change=UpdateParentFee(CRUDModel.ViewModel.FeeTypes[$parent.$index],$index,this)")]
        [CustomDisplay("Select")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Month")]
        public string MonthName { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("FeeDueAmount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("CreditNote")]
        public decimal? CreditNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("PrvCollected")]
        public decimal? PrvCollect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "", Attributes2 = "ng-blur=UpdateParentFee(CRUDModel.ViewModel.FeeTypes[$parent.$index],$index,this) ng-disabled=!gridModel.FeeDueMonthlyFinal[$index].IsRowSelected")]
        [MaxLength(15)]
        [CustomDisplay("Receivable")]
        public decimal? PayableAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes = "", Attributes2 = "ng-blur=UpdateParentFee(CRUDModel.ViewModel.FeeTypes[$parent.$index],$index,this) ng-disabled=!gridModel.FeeDueMonthlyFinal[$index].IsRowSelected")]
        [MaxLength(15)]
        [CustomDisplay("Refund")]
        public decimal? Refund { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Balance")]
        public decimal? Balance { get; set; }

       

    }
}
