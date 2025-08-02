using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentConcessionDetail", "CRUDModel.ViewModel.StudentConcessionDetail")]
    [DisplayName("Student Concession Fee Detail")]
    public class StudentConcessionDetailViewModel : BaseMasterViewModel
    {
        public StudentConcessionDetailViewModel()
        {
            FeePeriod = new KeyValueViewModel();
            FeeMaster = new KeyValueViewModel();
            //Months = new KeyValueViewModel();
            //Years = new KeyValueViewModel();
        }
        public long StudentFeeConcessionID { get; set; }      
        public long? FeeInvoiceID { get; set; }     
        public long? FeeDueFeeTypeMapID { get; set; }
        public long? FeeDueMonthlySplitID { get; set; }
        public long? CreditNoteFeeTypeMapID { get; set; }
        public long? CreditNoteID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("InvoiceNo", "Numeric", false, optionalAttribute1: "ng-disabled=true")]
        [CustomDisplay("InvoiceNo")]
        [LookUp("LookUps.InvoiceNo")]
        public KeyValueViewModel InvoiceNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", false, "", optionalAttribute1: "ng-disabled=true")]
        [LookUp("LookUps.FeePeriod")]
        [CustomDisplay("FeePeriod")]
        public KeyValueViewModel FeePeriod { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeMaster", "Numeric" , false, "", optionalAttribute1: "ng-disabled=true")]
        [LookUp("LookUps.FeeMaster")]
        [CustomDisplay("Fee Master")]
        public KeyValueViewModel FeeMaster { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Months", "Numeric", false, "GetYear($event, $element, gridModel)",false, "ng-click=GetFeeDueMonthlySplits(gridModel)")]
        //[CustomDisplay("Month")]
        //[LookUp("LookUps.Months")]
        //public KeyValueViewModel Months { get; set; }

        //public List<KeyValueViewModel> MonthList { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        ////[Select2("Years", "Numeric", false, "", false, optionalAttribute1: "ng-disabled=true")]
        //[Select2("Years", "Numeric", false, "", false)]
        //[CustomDisplay("Year")]
        //[LookUp("LookUps.Years")]
        //public KeyValueViewModel Years { get; set; }
        //public List<KeyValueViewModel> YearList { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, optionalAttribs:"ng-disabled=true")]
        [CustomDisplay("Due Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-blur=ConcessionPercentageChanges(gridModel)")]
        [CustomDisplay("Concession Percentage")]       
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        public decimal? ConcessionPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-blur=ConcessionAmountChanges(gridModel)")]
        [CustomDisplay("Concession Amount")]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        public decimal? ConcessionAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [CustomDisplay("Net To Pay")]
        public decimal? NetToPay { get; set; }
        public List<FeeDueMonthlySplitViewModel> MonthSplitList { get; set; }

    }
}
