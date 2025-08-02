using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeMasterMaps")]
    [DisplayName("Fee Structure Mapping")]
    public class FeeStructureFeeTypeViewModel : BaseMasterViewModel
    {
        public FeeStructureFeeTypeViewModel()
        {
            FeePeriod = new KeyValueViewModel();
            FeeMaster = new KeyValueViewModel();
            FeeMonthly = new List<FeeStructureMonthlySplitViewModel>() { new FeeStructureMonthlySplitViewModel() };
            IsFeePeriodDisabled = true;
            IsRowSelected = true;
        }
        public long FeeStructureID { get; set; }
        public long FeeStructureFeeMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Selection")]
        public bool? IsRowSelected { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeMaster", "Numeric", false, "FeeMasterChanges($event, $element, gridModel)", false)]
        [CustomDisplay("FeeMaster")]
        [LookUp("LookUps.FeeMaster")]
        public KeyValueViewModel FeeMaster { get; set; }

        public int? FeeMasterID { get; set; }
        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", false, "FeePeriodChanges($event, $element, gridModel)", optionalAttribute1: "ng-disabled=gridModel.IsFeePeriodDisabled")]
        [CustomDisplay("FeePeriods")]
        [LookUp("LookUps.FeePeriod")]
        public KeyValueViewModel FeePeriod { get; set; }

        public int? FeePeriodID { get; set; }

        public byte? FeeCycleID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='SplitAmount($event, $index, gridModel)'")]
        [CustomDisplay("Split")]
        public string Split { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.FeeMasterMaps[0], CRUDModel.ViewModel.FeeMasterMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.FeeMasterMaps[0],CRUDModel.ViewModel.FeeMasterMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes4 = "colspan=8")]
        [DisplayName("")]
        public List<FeeStructureMonthlySplitViewModel> FeeMonthly { get; set; }
    }
}

