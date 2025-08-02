using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExpenseEntryHeadViewModel", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Expense Details")]
    public class ExpenseEntryHeadViewModel : BaseMasterViewModel
    {
        public ExpenseEntryHeadViewModel()
        {
            DocumentType = new KeyValueViewModel();
            Currency = new KeyValueViewModel();
            PaymentModes = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            TaxDetails = new TaxDetailsViewModel();
        }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left")]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public string Branch { get; set; }
        public long? BranchID { get; set; }

        public long? CompanyID { get; set; }

        public long TransactionHeadIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)")]
        [DisplayName("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        public long? DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Exp. No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Currency")]
        [LookUp("LookUps.Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        public Nullable<long> DocumentStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PaymentModes", "Numeric", false, "DocumentTypeChange($event, $element)")]
        [LookUp("LookUps.PaymentModes")]
        [DisplayName("Payment Modes")]
        public KeyValueViewModel PaymentModes { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }
        public KeyValueViewModel DetailAccount { get; set; }
        public Nullable<long> DetailAccountID { get; set; }
        public double? Amount { get; set; }

        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }

        public string Allocate { get; set; }

        public Nullable<byte> TransactionStatusID { get; set; }

        public double? AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }

        public double? AmountPaid { get; set; }
        public long AccountTransactionHeadIID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public double? ExchangeRate { get; set; }
        public Nullable<bool> IsPrePaid { get; set; }
        public KeyValueViewModel TransactionStatus { get; set; }
        public KeyValueViewModel CostCenter { get; set; }
        
        //TODO: Need to check
        //[IgnoreMap]
        public TaxDetailsViewModel TaxDetails { get; set; }
    }
}
