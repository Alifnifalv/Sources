using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeePeriods", "CRUDModel.ViewModel.FeeMasterMaps")]
    [DisplayName("Fee Class Mapping")]
    public class StudentGroupFeeTypeMapViewmodel : BaseMasterViewModel
    {
        public StudentGroupFeeTypeMapViewmodel()
        {
            FeePeriod = new KeyValueViewModel();
            FeeMaster = new KeyValueViewModel();
            //FeeMonthly = new List<FeeMonthlySplitViewModel>() { new FeeMonthlySplitViewModel() };
            IsFeePeriodDisabled = true;
            IsRowSelected = true;
            IsPercentage = true;
        }
        public long StudentGroupFeeTypeMapIID { get; set; }

        public long StudentGroupFeeMasterID { get; set; }

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
        
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textright")]
        [DisplayName("Is Percentage")]
        public bool? IsPercentage { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [MaxLength(15)]
        [DisplayName(" % / Amount")]
        public decimal? PercentageAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Is Discount")]
        //public bool? IsDiscount { get; set; }

        

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Formula")]
        public string Formula { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        //[DisplayName("TaxPercentage")]
        //public decimal? TaxPercentage { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        //[DisplayName("Tax Amount")]
        //public decimal? TaxAmount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='SplitAmount($event, $index, gridModel)'")]
        //[DisplayName("Split")]
        //public string Split { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.FeeMasterMaps[0], CRUDModel.ViewModel.FeeMasterMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.FeeMasterMaps[0],CRUDModel.ViewModel.FeeMasterMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure")]
        //[DisplayName("")]
        //public List<FeeMonthlySplitViewModel> FeeMonthly { get; set; }
    }
}
