using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Domain.Entity.Models;
namespace Eduegate.Web.Library.Accounts.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "GeneralInvoiceTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class GeneralInvoiceMasterViewModel : BaseMasterViewModel
    {
        public GeneralInvoiceMasterViewModel()
        {
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.AccountSalesInvoice;
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType= new KeyValueViewModel();
            Currency = new KeyValueViewModel();
        }


        public long AccountTransactionHeadIID { get; set; }    

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left")]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public string Branch { get; set; }
        public long? BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CustomerAccount", "Numeric", false, "CustomerAccountChanges($event, $element)")]
        [LookUp("LookUps.CustomerAccount")]
        [CustomDisplay("Customer")]
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker /*, "", "DocumentTypeChange($event, $element)"*/)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PaymentModes", "Numeric", false)]
        [LookUp("LookUps.PaymentModes")]
        [CustomDisplay("PaymentMode")]
        public KeyValueViewModel PaymentModes { get; set; }      


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.DocumentType.Key")]
        [CustomDisplay("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }
        public long? DocumentTypeID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Invoice Number")]
        public string TransactionNo { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("Reference")]
        public string Reference { get; set; }
        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; } 
        
        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }
        
        //public Nullable<long> AccountID { get; set; }
        public KeyValueViewModel DetailAccount { get; set; }
        public Nullable<long> DetailAccountID { get; set; }
        public KeyValueViewModel TransactionStatus { get; set; }
        public decimal Amount { get; set; }
        public String Allocate { get; set; }
        public bool IsTransactionCompleted { get; set; }
        public KeyValueViewModel Currency { get; set; }
        public KeyValueViewModel CostCenter { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public double? ExchangeRate { get; set; }
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }

        public double? AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public double? AmountPaid { get; set; }
        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

    }
}
