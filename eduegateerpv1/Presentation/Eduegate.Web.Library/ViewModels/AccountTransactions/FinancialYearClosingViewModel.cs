using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Accounting.MonthlyClosing;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Students;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions.FinancialYearClosing
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FinancialYearClosing", "CRUDModel.ViewModel")]
    [DisplayName("Monthly Closing Details")]
    public class FinancialYearClosingViewModel : BaseMasterViewModel
    {
        public FinancialYearClosingViewModel()
        {
            Details = new FinancialYearClosingDetailViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, Attributes = "ng-change=CompanyDetails()")]
        [CustomDisplay("Company")]
        [LookUp("LookUps.Company")]
        public string Company { get; set; }

        public int? CompanyID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [DisplayName("")]
        public FinancialYearClosingDetailViewModel Details { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textleft fontbolder")]
        //[CustomDisplay("Previous Financial Year")]
        //public string PreviousFinancialYear { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "textleft fontbolder")]
        //[CustomDisplay("Current Financial Year")]
        //public string CurrentFinancialYear { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine1 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.FiscalYear")]
        //[CustomDisplay("Financial Year")]
        //public string PreviousFinancialYearString { get; set; }
        //public int? PreviousFinancialYearID { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.FiscalYear")]
        //[CustomDisplay("Financial Year")]
        //public string CurrentFinancialYearString { get; set; }
        //public int? CurrentFinancialYearID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine2 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change=CheckDateRanges()")]
        //[CustomDisplay("From Date")]
        //public string StartDateString { get; set; }

        //public DateTime? StartDate { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change=CheckDateRanges()")]
        //[CustomDisplay("To Date")]
        //public string EndDateString { get; set; }
        //public DateTime? EndDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine3 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.FiscalYear")]
        //[CustomDisplay("Audit Status")]
        //public string PrFYAuditStatusString { get; set; }
        //public int? PrFYAuditStatusID { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.FiscalYear")]
        //[CustomDisplay("Financial Year")]
        //public string CFYAuditStatusString { get; set; }
        //public int? CFYAuditStatusID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeGeneralMonthlyClosingDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FinancialYearClosingViewModel>(jsonString);
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeGeneralMonthlyClosingDTO>(jsonString);
        }

    }
}
