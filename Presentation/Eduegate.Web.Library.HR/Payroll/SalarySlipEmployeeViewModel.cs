using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SalarySlipEmployee", "CRUDModel.ViewModel.EmployeeList")]
    [DisplayName("Employee List")]
    public class SalarySlipEmployeeViewModel : BaseMasterViewModel
    {
        public SalarySlipEmployeeViewModel()
        {
            SalarySlipReviewSplit = new List<SalarySlipReviewSplitViewModel>() { new SalarySlipReviewSplitViewModel() };
        }

        public long SalarySlipIID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width")]
        [CustomDisplay("Selected")]
        public bool IsSelected { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "large-col-width alignleft")]
        //[CustomDisplay("Date")]
        public string SlipDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width alignleft")]
        [CustomDisplay("EmployeeName")]
        public string EmployeeName { get; set; }
        public long? EmployeeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "textleft medium-col-width")]
        [CustomDisplay("Working Days")]
        public decimal? WorkingDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft medium-col-width")]
        [CustomDisplay("Normal Hours")]
        public decimal? NormalHrs { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft medium-col-width")]
        [CustomDisplay("OT Hours")]
        public decimal? OTHrs { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft small-col-width")]
        [CustomDisplay("LOP Days")]
        public decimal? LOPDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width")]
        [CustomDisplay("Verified")]
        public bool? IsVerified { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PublishStatus", "Numeric", false)]
        [LookUp("LookUps.PublishStatus")]
        [CustomDisplay("Publish Status")]
        public KeyValueViewModel PublishStatus { get; set; }
        public int? PublishStatusID { get; set; }

        public string EmailAddress { get; set; }

        public long? ReportContentID { get; set; }

        public decimal? EarningAmount { get; set; }

        public decimal? DeductingAmount { get; set; }

        public decimal? AmountToPay { get; set; }

        public long? BranchID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "MarkRegSkillSplit", Attributes2 = "colspan=9")]
        [DisplayName("")]
        public List<SalarySlipReviewSplitViewModel> SalarySlipReviewSplit { get; set; }

    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SalarySlipReviewSplit", "gridModel.SalarySlipReviewSplit")]
    [DisplayName("Salary Split")]
    public class SalarySlipReviewSplitViewModel : BaseMasterViewModel
    {

        public SalarySlipReviewSplitViewModel()
        {


        }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright small-col-width")]
        [CustomDisplay("Earnings")]
        public decimal Earnings { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright small-col-width")]
        [CustomDisplay("Deductions")]
        public decimal Deductions { get; set; }

    }

}