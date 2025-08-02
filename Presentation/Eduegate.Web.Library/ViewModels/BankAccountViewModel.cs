using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBankAccounts", "BankAccounts", "", "", "", "bankAccountGridRow")]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "BankAccounts", "CRUDModel.ViewModel.SupplierBankAccounts")]
    [DisplayName("Bank Accounts")]
    public class BankAccountViewModel : BaseMasterViewModel
    {
        public BankAccountViewModel() {

            BankAttachment = new List<BankAttachmentsViewModel>() { new BankAttachmentsViewModel() };
        }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Account Holder Name")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string AccountHolderName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Account No")]
        public string AccountNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IBAN")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string IBAN { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Swift/BIC Code")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string SwiftCode { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Credit References")]
        public bool? IsCreditReference { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth")]
        [CustomDisplay("Payment Terms :")]
        public string Label2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Max Number of days allowed")]
        //[MaxLength(3, ErrorMessage = "Maximum Length should be within 3!")]
        public int? PaymentMaxNoOfDaysAllowed { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Upload Documents")]
        public List<BankAttachmentsViewModel> BankAttachment { get; set; }
    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "BankAttachment", "CRUDModel.ViewModel.SupplierBankAccounts.BankAttachment")]
    [DisplayName("")]
    public class BankAttachmentsViewModel : BaseMasterViewModel
    {
        public BankAttachmentsViewModel() {

        }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Letter of confirmation from the Bank")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "LetterConfirmationFromBank", "")]
        public long? LetterConfirmationFromBank { get; set; }
        public string LetterConfirmationFromBankID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Latest audited financial statement")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "LatestAuditedFinancialStatements", "")]
        public long? LatestAuditedFinancialStatements { get; set; }
        public string LatestAuditedFinancialStatementsID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Liability Insurance")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "LiabilityInsurance", "")]
        public long? LiabilityInsurance { get; set; }
        public string LiabilityInsuranceID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Workers' Compensation Insurance")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "WorkersCompensationInsurance", "")]
        public long? WorkersCompensationInsurance { get; set; }
        public string WorkersCompensationInsuranceID { get; set; }
    }
}
