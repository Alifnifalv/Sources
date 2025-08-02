using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "BranchTransferMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Asset Transfer Details")]
    public class AssetTransferReceiptMasterViewModel : BaseMasterViewModel
    {
        public AssetTransferReceiptMasterViewModel()
        {
            DocumentReferenceTypeID = DocumentReferenceTypes.AssetTransferReceipt;
            DocumentStatus = new KeyValueViewModel();
        }

        public long TransactionHeadIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("ATR Number")]
        [DataPicker("AssetTransfer")]
        public string ReferenceTransactionNo { get; set; }
        public string ReferenceTransactionHeaderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false)]
        [CustomDisplay("FromBranch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }
        public long? BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "onecol-header-left")]
        [CustomDisplay("ToBranch")]
        [LookUp("LookUps.Branch")]
        public string ToBranch { get; set; }
        public long? ToBranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.DocumentType.Key")]
        [CustomDisplay("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }
        public long? DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("ATNo")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public string TransactionDateString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remark")]
        public string Remarks { get; set; }

        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }

        private int? documentReferenceTypeID;
        public bool IsTransactionCompleted { get; set; }
        public int? CompanyID { get; set; }
    }
}