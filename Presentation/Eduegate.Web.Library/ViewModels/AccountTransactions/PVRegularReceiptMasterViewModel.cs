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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PVRegularPaymentTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class PVRegularPaymentMasterViewModel : BaseMasterViewModel
    {
        public PVRegularPaymentMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            //DocumentType= new KeyValueViewModel();
            DocumentTypeID = 6;
            DocumentType = new KeyValueViewModel() { Key = DocumentTypeID.ToString(), Value = "Payment" };
            ChequePaymentModeIDFromSetting = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("CHEQUE_PAYMENT_MODE_ID"));
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
        [ControlType(Framework.Enums.ControlTypes.DatePicker /*, "", "DocumentTypeChange($event, $element)"*/)]
        [CustomDisplay("PVDate")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PaymentModes", "Numeric", false, "PaymentModeChange($event, $element, CRUDModel.Model.MasterViewModel)")]
        [LookUp("LookUps.PaymentModes")]
        [CustomDisplay("PaymentModes")]
        public KeyValueViewModel PaymentModes { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("PVNumber")]
        public String TransactionNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.Model.MasterViewModel.PaymentModes.Key != CRUDModel.Model.MasterViewModel.ChequePaymentModeIDFromSetting'")]
        [CustomDisplay("ChequeNumber")]
        public string ChequeNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentType", "Numeric", false, "", false, optionalAttribute1: "ng-disabled='true'")]
        [LookUp("LookUps.DocumentType")]
        [CustomDisplay("PVType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='CRUDModel.Model.MasterViewModel.PaymentModes.Key != CRUDModel.Model.MasterViewModel.ChequePaymentModeIDFromSetting'")]
        [CustomDisplay("ChequeDate")]
        public string ChequeDateString { get; set; }

        public KeyValueViewModel Currency { get; set; }
        public KeyValueViewModel CostCenter { get; set; }

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

        public int? ChequePaymentModeIDFromSetting { get; set; }
    }
}