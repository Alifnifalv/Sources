using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeTypes")]
    [DisplayName("FeeTypes")]
    public class FeePaymentFeeTypeViewModel : BaseMasterViewModel
    {
        public FeePaymentFeeTypeViewModel()
        {
            //Slider = new SliderViewModel();
            FeeMonthly = new List<FeeAssignMonthlySplitViewModel>() { new FeeAssignMonthlySplitViewModel() };
        }

        public bool? IsExternal { get; set; }

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

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [DisplayName("FeeMaster")]
        public string FeeMaster { get; set; }
        public int? FeeMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Fee Type")]
        public string FeePeriod { get; set; }
        public long? FeePeriodID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Due Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textright")]
        [DisplayName("Paying Now")]        
        public bool IsPayingNow { get; set; }

        public long FeeCollectionFeeTypeMapsIID { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public long? FeeMasterClassMapID { get; set; }

        public decimal? NowPaying { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes2 = "colspan=10")]
        [DisplayName("")]
        public List<FeeAssignMonthlySplitViewModel> FeeMonthly { get; set; }

    }
}