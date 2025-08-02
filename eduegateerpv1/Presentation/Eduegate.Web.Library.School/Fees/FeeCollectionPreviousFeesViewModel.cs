using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "PreviousFees", "CRUDModel.ViewModel.PreviousFees")]
    [DisplayName("PreviousFees")]
    public class FeeCollectionPreviousFeesViewModel : BaseMasterViewModel
    {
        public FeeCollectionPreviousFeesViewModel()
        {
            IsFeePeriodDisabled = true;            
            FeeMonthly = new List<FeeAssignMonthlySplitViewModel>() { new FeeAssignMonthlySplitViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Invoice No")]
        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Invoice Date")]
        public string InvoiceDateString { get; set; }
        public DateTime? InvoiceDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Fee")]
        public string FeeMaster { get; set; }
        public byte? FeeCycleID { get; set; }

        public int? FeeMasterID { get; set; }

        public int? FineMasterID { get; set; }
        public long? FineMasterStudentMapID { get; set; }

        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Fee Period")]
        public string FeePeriod { get; set; }

      
        public int? FeePeriodID { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Fee Due Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Credit Note")]
        public decimal? CreditNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Balance")]
        public decimal? Balance { get; set; }

        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? FeeStructureFeeMapID { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-disabled= 'gridModel.TaxAmount.length != 0'")]
        //[DisplayName("Tax %(Fee Master)")]
        //public decimal? TaxPercentage { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        //[DisplayName("Tax")]
        //public decimal? TaxAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textright", Attributes = "ng-bind='AddNumber(gridModel.Amount,gridModel.TaxAmount)'")]
        //[DisplayName("Total")]
        //public decimal? TotalAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='SplitAmount($event, $index, gridModel)'")]
        //[DisplayName("Split")]
        //public string Split { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.FeeTypes[0], CRUDModel.ViewModel.FeeTypes)'")]
        //[DisplayName("+")]
        //public string Add { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.FeeTypes[0],CRUDModel.ViewModel.FeeTypes)'")]
        //[DisplayName("-")]
        //public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes2 = "colspan=10")]
        [DisplayName("")]
        public List<FeeAssignMonthlySplitViewModel> FeeMonthly { get; set; }
    }
}

   