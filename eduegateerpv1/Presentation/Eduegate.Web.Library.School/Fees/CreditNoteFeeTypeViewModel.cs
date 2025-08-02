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
    [DisplayName("FeeAssign")]
    public class CreditNoteFeeTypeViewModel : BaseMasterViewModel
    {
        public CreditNoteFeeTypeViewModel()
        {
            FeeMaster = new KeyValueViewModel();
            FeePeriod = new KeyValueViewModel();
            Months = new KeyValueViewModel();
            Years = new KeyValueViewModel();
            MonthList = new List<KeyValueViewModel>();
            YearList = new List<KeyValueViewModel>();
            MonthSplitList = new List<FeeDueMonthlySplitViewModel>() { new FeeDueMonthlySplitViewModel() };

            ScreenID = 2297;
        }

        public long CreditNoteFeeTypeMapIID { get; set; }
        public long? SchoolCreditNoteID { get; set; }
        public bool Status { get; set; }
        public bool IsRegularFee { get; set; }
        public int? FeeMasterID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeMaster", "Numeric", false, "FeeMasterChangesFromCredit($event, $element, gridModel)", false)]
        [CustomDisplay("Fee")]
        [LookUp("LookUps.FeeMaster")]
        public KeyValueViewModel FeeMaster { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", false, "GetInvoiceNo($event, $element, gridModel)", optionalAttribute1: "ng-disabled=gridModel.IsFeePeriodDisabled")]
        [CustomDisplay("FeePeriod")]
        [LookUp("LookUps.FeePeriod")]
        public KeyValueViewModel FeePeriod { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("InvoiceNo", "Numeric", false, "SplitUpPeriodMonthYear($event, $element, gridModel,CRUDModel.ViewModel)")]
        [CustomDisplay("InvoiceNo")]
        [LookUp("LookUps.InvoiceNo")]
        public KeyValueViewModel InvoiceNo { get; set; }

        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Months", "Numeric", false,  "GetYear($event, $element, gridModel)", optionalAttribute1: "ng-disabled=gridModel.IsFeePeriodDisabled")]
        [CustomDisplay("Month")]
        [LookUp("LookUps.Months")]
        public KeyValueViewModel Months { get; set; }

        public List<KeyValueViewModel> MonthList { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Years", "Numeric", false, "", false, optionalAttribute1: "ng-disabled=true")]
        [Select2("Years", "Numeric", false, "",optionalAttribute1: "ng-disabled=gridModel.IsFeePeriodDisabled")]
        [CustomDisplay("Year")]
        [LookUp("LookUps.Years")]
        public KeyValueViewModel Years { get; set; }
        public List<KeyValueViewModel> YearList { get; set; }

        public List<FeeDueMonthlySplitViewModel> MonthSplitList { get; set; }

        public long? FeeDueMonthlySplitID { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        public decimal? DueAmount { get; set; }

        public int? ScreenID { get; set; }
                
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.FeeTypes[0], CRUDModel.ViewModel.FeeTypes)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.FeeTypes[0],CRUDModel.ViewModel.FeeTypes)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
