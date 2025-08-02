using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;
using System.Globalization;


namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeTypes")]
    [DisplayName("FeeAssign")]
    public class FeeCollectionFeeTypeViewModel : BaseMasterViewModel
    {
        public FeeCollectionFeeTypeViewModel()
        {
            IsFeePeriodDisabled = true;
           // IsRowSelected = true;
            //FeePeriod = new KeyValueViewModel();
            //FeeMaster = new KeyValueViewModel();
            FeeMonthly = new List<FeeAssignMonthlySplitViewModel>() { new FeeAssignMonthlySplitViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", Attributes = "ng-change=ClearSelection(gridModel)")]
        [CustomDisplay("Select")]
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
        //public byte? FeeCycleID { get; set; }
      
        public int? FeeMasterID { get; set; }

        public int? FineMasterID { get; set; }
        public long? FineMasterStudentMapID { get; set; }

        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("FeePeriod")]
        public string FeePeriod { get; set; }
             

        public int? FeePeriodID { get; set; }

        // [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Credit/DebitNote")]
        public decimal? CreditNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("PrvCollected")]
        public decimal? PrvCollect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width", Attributes3 = "ng-blur=SplitFeeAmount(gridModel)")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("NowPaying")]
        public decimal? NowPaying { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Balance")]
        public decimal? Balance { get; set; }
        

        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? FeeStructureFeeMapID { get; set; }

        public long? CreditNoteFeeTypeMapID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes2 = "colspan=10")]
        [DisplayName("")]
        public List<FeeAssignMonthlySplitViewModel> FeeMonthly { get; set; }
    }
}
