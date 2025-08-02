using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SlipComponents", "CRUDModel.ViewModel.SlipComponents")]
    [DisplayName("SalarySlip")]
    public class SalarySlipComponentViewModel : BaseMasterViewModel
    {
        public SalarySlipComponentViewModel()
        {
            SalaryComponent = new KeyValueViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryComponent", "Numeric", false, "")]
        [LookUp("LookUps.SalaryComponents")]
        [DisplayName("Components")]
        public KeyValueViewModel SalaryComponent { get; set; }

        public long SalarySlipIID { get; set; }

        public int? SalaryComponentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [DisplayName("Earnings")]
        public decimal? Earnings { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [DisplayName("Deduction")]
        public decimal? Deduction { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Description")]
        public string Description { get; set; }

       
        public decimal? NoOfDays { get; set; }

        public decimal? NoOfHours { get; set; }
   
        public bool? IsVerified { get; set; }
  
        public long? ReportContentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.SlipComponents[0], CRUDModel.ViewModel.SlipComponents)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SlipComponents[0],CRUDModel.ViewModel.SlipComponents)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}