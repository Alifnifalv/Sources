using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeTypes")]
    [DisplayName("FeeAssign")]
    public class FeeCollectionHistoryFeeTypeViewModel : BaseMasterViewModel
    {
        public FeeCollectionHistoryFeeTypeViewModel()
        {
            IsFeePeriodDisabled = true;
            IsRowSelected = true;
            FeePeriod = new KeyValueViewModel();
            FeeMaster = new KeyValueViewModel();
            FeeMonthly = new List<FeeAssignMonthlySplitViewModel>() { new FeeAssignMonthlySplitViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]
        [DisplayName("Selection")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Fee Receipt No")]
        public string FeeReceiptNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Collection Date")]
        public string CollectionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeMaster", "Numeric", false, "FeeMasterChanges($event, $element, gridModel)", false, optionalAttribute1: "ng-disabled=true")]
        [DisplayName("Fee Master")]
        [LookUp("LookUps.FeeMaster")]
        public KeyValueViewModel FeeMaster { get; set; }
        public int? FeeMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", false, "FeePeriodChanges($event, $element, gridModel)", optionalAttribute1: "ng-disabled=gridModel.IsFeePeriodDisabled")]
        [DisplayName("Fee Periods")]
        [LookUp("LookUps.FeePeriod")]
        public KeyValueViewModel FeePeriod { get; set; }
        public int? FeePeriodID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }

        public bool IsFeePeriodDisabled { get; set; }

        public byte? FeeCycleID { get; set; }

        public long FeeCollectionFeeTypeMapsIID { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public long? FeeMasterClassMapID { get; set; }

        public int? FeeCollectionStatusID { get; set; }
        public string FeeCollectionStatus { get; set; }

        public int? FeeCollectionDraftStatusID { get; set; }

        public int? FeeCollectionCollectedStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes2 = "colspan=10")]
        [DisplayName("")]
        public List<FeeAssignMonthlySplitViewModel> FeeMonthly { get; set; }

    }
}