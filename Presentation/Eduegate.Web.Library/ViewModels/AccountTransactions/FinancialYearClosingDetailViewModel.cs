using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Web.Library.Common;
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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FinancialYearClosingDetail", "CRUDModel.ViewModel.Details")]
    [DisplayName("Monthly Closing Details")]
    public class FinancialYearClosingDetailViewModel : BaseMasterViewModel
    {
        public FinancialYearClosingDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft fontbolder")]
        [CustomDisplay("Previous Financial Year")]
        public string PreviousFinancialYear { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft fontbolder")]
        [CustomDisplay("Current Financial Year")]
        public string CurrentFinancialYear { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.FiscalYear")]
        [CustomDisplay("Financial Year")]
        public string PreviousFinancialYearString { get; set; }
        public int? PreviousFinancialYearID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.FiscalYear")]
        [CustomDisplay("Financial Year")]
        public string CurrentFinancialYearString { get; set; }
        public int? CurrentFinancialYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("From Date")]
        public string PrStartDateString { get; set; }

        public DateTime? PrStartDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("From Date")]
        public string CrStartDateString { get; set; }

        public DateTime? CrStartDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("To Date")]
        public string PrEndDateString { get; set; }
        public DateTime? PrEndDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("To Date")]
        public string CrEndDateString { get; set; }
        public DateTime? CrEndDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.FiscalYear")]
        [CustomDisplay("Audit Status")]
        public string PrFYAuditStatusString { get; set; }
        public int? PrFYAuditStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.FiscalYear")]
        [CustomDisplay("Audit Status")]
        public string CFYAuditStatusString { get; set; }
        public int? CFYAuditStatusID { get; set; }

    }
}
