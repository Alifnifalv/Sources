using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RegularCreditTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class RegularCreditMasterViewModel : BaseMasterViewModel
    {
        public RegularCreditMasterViewModel()
        {
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.CreditNote;
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType= new KeyValueViewModel();
        }


        public long AccountTransactionHeadIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public double? ExchangeRate { get; set; }
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        
        public double? AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public double? AmountPaid { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left")]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public string Branch { get; set; }
        public long? BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker /*, "", "ng-change='GetNextTransactionNumber(CRUDModel.Model)'"*/)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AccountPaymentMode", "Numeric", false, "DocumentTypeChange($event, $element)")]
        [LookUp("LookUps.AccountPaymentMode")]
        [CustomDisplay("PaymentMode")]
        public KeyValueViewModel PaymentModes { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentType", "Numeric", false, "", false, optionalAttribute1: "ng-disabled='true'")]
        [LookUp("LookUps.DocumentType")]
        [CustomDisplay("Type")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }


        public KeyValueViewModel Currency { get; set; }
        public KeyValueViewModel CostCenter { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TransactionNumber")]
        public String TransactionNumber { get; set; }

        public KeyValueViewModel TransactionStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; }

       
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }
        public KeyValueViewModel DetailAccount { get; set; }
        public Nullable<long> DetailAccountID { get; set; }
        public decimal Amount { get; set; }
        public String Allocate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public bool IsTransactionCompleted { get; set; }

        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

    }
}
