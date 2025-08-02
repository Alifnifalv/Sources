using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeMonthly", "gridModel.FeeMonthly")]
    [DisplayName("FeeAssign")]
    public class FeeAssignMonthlySplitViewModel : BaseMasterViewModel
    {
        public long MapIID { get; set; }

        public long? FeeMasterClassMapID { get; set; }

        public long?  FeeDueMonthlySplitID { get; set; }

        public long?  FeeCollectionFeeTypeMapId { get; set; }

        public long? CreditNoteFeeTypeMapID { get; set; }

        public bool? FeeCollectionStatus { get; set; }

        public long? ParentID { get; set; }

        public int MonthID { get; set; }

        public int Year { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", Attributes = "ng-change=UpdateParentFee(CRUDModel.ViewModel.FeeTypes[$parent.$index],$index)")]
        [CustomDisplay("Select")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Month")]
        public string MonthName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("FeeDueAmount")]
        public decimal? Amount { get; set; }
        public decimal? TotalAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Credit/DebitNote")]
        public decimal? CreditNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("PrvCollected")]
        public decimal? PrvCollect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("NowPaying")]
        public decimal? NowPaying { get; set; }

        public decimal? NowPayingOld { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Balance")]
        public decimal? Balance { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        //[DisplayName("Tax %(Fee Master)")]
        //public decimal? TaxPercentage { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        //[DisplayName("Tax")]
        //public decimal? TaxAmount { get; set; }

        public decimal? OldNowPaying { get; set; }

        public bool IsExpand { get; set; }

    }
}