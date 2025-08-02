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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeePeriods", "CRUDModel.ViewModel.FeeMasterMaps")]
    [DisplayName("Fee Class Mapping")]
    public class FeeMasterClassFeePeriodViewModel:BaseMasterViewModel
    {
        public FeeMasterClassFeePeriodViewModel()
        {
            FeePeriod = new KeyValueViewModel();
            FeeMaster =  new KeyValueViewModel();
            FeeMonthly = new List<FeeMonthlySplitViewModel>() { new FeeMonthlySplitViewModel() };
            IsFeePeriodDisabled = true;
            IsRowSelected = true;
        }
        public long? ClassFeeMasterID { get; set; }
        public long FeeMasterClassMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Selection")]
        public bool? IsRowSelected { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeMaster", "Numeric", false, "FeeMasterChanges($event, $element, gridModel)", false)]
        [DisplayName("Fee Master")]
        [LookUp("LookUps.FeeMaster")]
        public KeyValueViewModel FeeMaster { get; set; }

        public int? FeeMasterID { get; set; }
        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", false, "FeePeriodChanges($event, $element, gridModel)", optionalAttribute1: "ng-disabled=gridModel.IsFeePeriodDisabled")]
        [DisplayName("Fee Periods")]
        [LookUp("LookUps.FeePeriod")]
        public KeyValueViewModel FeePeriod { get; set; }

        public int? FeePeriodID { get; set; }

        public byte? FeeCycleID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }

        ////[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-disabled= 'gridModel.TaxAmount.length != 0'")]
        //[DisplayName("Tax %")]
        //public decimal? TaxPercentage { get; set; }

        ////[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-focus='gridModel.TaxAmount = GetAmountByPercentage(gridModel.Amount,gridModel.TaxPercentage)'")]
        //[DisplayName("Tax Amount")]
        //public decimal? TaxAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textright", Attributes = "ng-bind='AddNumber(gridModel.Amount,gridModel.TaxAmount)'")]
        //[DisplayName("Total")]
        //public decimal? TotalAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='SplitAmount($event, $index, gridModel)'")]
        [DisplayName("Split")]
        public string Split { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.FeeMasterMaps[0], CRUDModel.ViewModel.FeeMasterMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.FeeMasterMaps[0],CRUDModel.ViewModel.FeeMasterMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes2 = "colspan=10")]
        [DisplayName("")]
        public List<FeeMonthlySplitViewModel> FeeMonthly { get; set; }
    }
}
