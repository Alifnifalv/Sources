using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Domain;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RVInvoiceTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class RVInvoiceMasterViewModel : BaseMasterViewModel
    {
        public RVInvoiceMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType= new KeyValueViewModel();
            ChequePaymentModeIDFromSetting = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("CHEQUE_PAYMENT_MODE_ID"));
        }

        public long AccountTransactionHeadIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
       
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<int> DocumentReferenceTypeID { get; set; }

        public double? AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public double? AmountPaid { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left")]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public string Branch { get; set; }
        public long? BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker /*,"", "DocumentTypeChange($event, $element)"*/)]
        [CustomDisplay("RVDate")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PaymentModes", "Numeric", false, "DocumentTypeChange($event, $element)")]
        [LookUp("LookUps.PaymentModes")]
        [CustomDisplay("PaymentModes")]
        public KeyValueViewModel PaymentModes { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentType", "Numeric",false, "", false, optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.DocumentType.Key")]
        [LookUp("LookUps.DocumentType")]
        [CustomDisplay("RVType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.Model.MasterViewModel.PaymentModes.Key != CRUDModel.Model.MasterViewModel.ChequePaymentModeIDFromSetting'")]
        [CustomDisplay("ChequeNumber")]
        public string ChequeNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("RVNumber")]
        public String TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='CRUDModel.Model.MasterViewModel.PaymentModes.Key != CRUDModel.Model.MasterViewModel.ChequePaymentModeIDFromSetting'")]
        [CustomDisplay("ChequeDate")]
        public string ChequeDateString { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[DisplayName("Currency")]
        //[LookUp("LookUps.Currency")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("ExchangeRate")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        public double? ExchangeRate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CostCenter", "Numeric", false)]
        [CustomDisplay("CostCenter")]
        [LookUp("LookUps.CostCenterDetails")]
        public KeyValueViewModel CostCenter { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CustomerAccounts", "Numeric", false, "LoadCustomerPendingInvoices($event, $element)")]
        [CustomDisplay("Customer")]        
        [LookUp("LookUps.CustomerAccount")]
        public KeyValueViewModel DetailAccount { get; set; }

        public Nullable<long> DetailAccountID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Debit Accounts")]
        [Select2("DebitAccounts", "Numeric")]
        [LazyLoad("", "Accounts/RVMission/GetChildAccounts_ByParentAccountId", "LookUps.DebitAccounts")]
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "class='textright' ng-blur='AllocationAmountChange($event, $element,CRUDModel.Model)'")]
        [CustomDisplay("Amount")]
        public decimal Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click ='AllocateAmount(CRUDModel.Model)' ng-hide='(CRUDModel.Model.MasterViewModel.Amount<=0)'")]
        [CustomDisplay("Allocate")]
        public bool Allocate { get; set; }

        public KeyValueViewModel TransactionStatus { get; set; }

        public bool IsTransactionCompleted { get; set; }

        public int? ChequePaymentModeIDFromSetting { get; set; }
    }
}