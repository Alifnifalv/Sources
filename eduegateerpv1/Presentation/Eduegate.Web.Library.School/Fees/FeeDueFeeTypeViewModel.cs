using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeDueMaps", "CRUDModel.ViewModel.FeeDueMaps")]
    [DisplayName("Fee Due Fee Period Mapping")]
  public  class FeeDueFeeTypeViewModel : BaseMasterViewModel
    {
        public FeeDueFeeTypeViewModel()
        {
            IsFeePeriodDisabled = true;
            IsRowSelected = true;
            FeePeriod = new KeyValueViewModel();
            FeeMaster = new KeyValueViewModel();
            FeeDueMonthly = new List<FeeDueMonthlySplitViewModel>() { new FeeDueMonthlySplitViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", optionalAttribs: "ng-disabled=gridModel.FeeCollectionStatus == true")]
        [DisplayName("Selection")]
        public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]
        public bool FeeCollectionStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeMaster", "Numeric", false, "FeeMasterChanges($event, $element, gridModel)", optionalAttribute1: "ng-disabled=gridModel.FeeCollectionStatus == true")]
        [DisplayName("Fee Master")]
        [LookUp("LookUps.FeeMaster")]
        public KeyValueViewModel FeeMaster { get; set; }
        public byte? FeeCycleID { get; set; }

        public int? FeeMasterID { get; set; }

        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", false, "FeePeriodChanges($event, $element, gridModel)", optionalAttribute1: "ng-disabled='gridModel.IsFeePeriodDisabled || gridModel.FeeCollectionStatus == true'")]
        [DisplayName("Fee Periods")]
        [LookUp("LookUps.FeePeriod")]
        public KeyValueViewModel FeePeriod { get; set; }

        public int? FeePeriodID { get; set; }

        // [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright","", optionalAttribs: "ng-disabled = gridModel.FeeCollectionStatus == true")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='SplitAmount($event, $index, gridModel)'", optionalAttribs: "ng-disabled=gridModel.FeeCollectionStatus == true")]
        [DisplayName("Split")]
        public string Split { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.FeeDueMaps[0], CRUDModel.ViewModel.FeeDueMaps)'", optionalAttribs: "ng-disabled=$parent.CollectionStatus")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.FeeDueMaps[0],CRUDModel.ViewModel.FeeDueMaps)'", optionalAttribs: "ng-disabled=gridModel.Status")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure")]
        [DisplayName("")]
        public List<FeeDueMonthlySplitViewModel> FeeDueMonthly { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long?  FeeStructureFeeMapID { get; set; }
        //public long? FeeMasterClassMapID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]

        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]

        public string InvoiceDate { get; set; }

    }
}