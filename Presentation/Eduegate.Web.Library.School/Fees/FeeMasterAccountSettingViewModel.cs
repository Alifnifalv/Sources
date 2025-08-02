using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;
using System.Runtime.InteropServices;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AccountSetting", "CRUDModel.ViewModel.AccountSetting")]
    [DisplayName("Account Settings")]
    public class FeeMasterAccountSettingViewModel : BaseMasterViewModel
    {
        public FeeMasterAccountSettingViewModel()
        {
            LedgerAccount = new KeyValueViewModel();
            TaxLedgerAccount = new KeyValueViewModel();
            OutstandingAccount = new KeyValueViewModel();
            OSTaxAccount = new KeyValueViewModel();
            AdvanceAccount = new KeyValueViewModel();
            AdvanceTaxAccount = new KeyValueViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("LedgerAccount")]
        [LookUp("LookUps.LedgerAccount")]
        [Select2("LedgerAccount", "Numeric", false, "")]
        public KeyValueViewModel LedgerAccount { get; set; }

        public long? LedgerAccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("TaxLedgerAccount")]
        [LookUp("LookUps.TaxLedgerAccount")]
        [Select2("TaxLedgerAccount", "Numeric", false, "")]
        public KeyValueViewModel TaxLedgerAccount { get; set; }

        public long? TaxLedgerAccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [CustomDisplay("TaxPercentage")]
        public decimal? TaxPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Outstanding")]
        [LookUp("LookUps.LedgerAccount")]
        [Select2("OutstandingAccount", "Numeric", false, "")]
        public KeyValueViewModel OutstandingAccount { get; set; }

        public long? OutstandingAccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("TaxLedgerAccount")]
        [LookUp("LookUps.TaxLedgerAccount")]
        [Select2("OSTaxAccount", "Numeric", false, "")]
        public KeyValueViewModel OSTaxAccount { get; set; }

        public long? OSTaxAccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [CustomDisplay("TaxPercentage")]
        public decimal? OSTaxPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Advance")]
        [LookUp("LookUps.LedgerAccount")]
        [Select2("AdvanceAccount", "Numeric", false, "")]
        public KeyValueViewModel AdvanceAccount { get; set; }

        public long? AdvanceAccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("TaxLedgerAccount")]
        [LookUp("LookUps.TaxLedgerAccount")]
        [Select2("AdvanceTaxAccount", "Numeric", false, "")]
        public KeyValueViewModel AdvanceTaxAccount { get; set; }

        public long? AdvanceTaxAccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [CustomDisplay("TaxPercentage")]
        public decimal? AdvanceTaxPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Provision for Advance")]
        [LookUp("LookUps.LedgerAccount")]
        [Select2("ProvisionforAdvance", "Numeric", false, OptionalAttribute1 = "ng-disabled='CRUDModel.ViewModel.FeeCycle != 5'")]
        public KeyValueViewModel ProvisionforAdvanceAccount { get; set; }

        public long? ProvisionforAdvanceAccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Provision for Outstanding")]
        [LookUp("LookUps.LedgerAccount")]
        [Select2("ProvisionforOutstanding", "Numeric", false, OptionalAttribute1 = "ng-disabled='CRUDModel.ViewModel.FeeCycle != 5'")]
        public KeyValueViewModel ProvisionforOutstandingAccount { get; set; }

        public long? ProvisionforOutstandingAccountID { get; set; }
    }
}
