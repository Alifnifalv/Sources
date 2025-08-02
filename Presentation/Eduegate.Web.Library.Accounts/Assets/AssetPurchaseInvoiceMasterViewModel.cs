using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetPurchaseInvoiceMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Invoice Details")]
    public class AssetPurchaseInvoiceMasterViewModel : BaseMasterViewModel
    {
        public AssetPurchaseInvoiceMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
        }

        public long TransactionHeadIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("PO Number")]
        [DataPicker("AssetPurchaseOrderAdvanceSearch", true)]
        public string POReferenceTransactionNo { get; set; }
        public string POReferenceTransactionHeadID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false, optionalAttribute1: "ng-disabled='CRUDModel.Model.MasterViewModel.TransactionHeadIID !=0'")]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }
        public long? BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "Numeric", false, "DocumentTypeChange($event, $element)", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.DocumentType.Key")]
        [CustomDisplay("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }
        public long? DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Trans No")]
        public string TransactionNo { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[DisplayName("PI Number")]
        //[DataPicker("PurchaseInvoiceAdvanceSearch", true)]
        public string PIReferenceTransactionNo { get; set; }
        public string PIReferenceTransactionHeadID { get; set; }

        public long? ReferenceTransactionHeaderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public string TransactionDateString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }
        public long DocumentStatusID { get; set; }

        public KeyValueViewModel Supplier { get; set; }
        public long SupplierID { get; set; }

        public KeyValueViewModel TransactionStatus { get; set; }

        public byte EntitlementID { get; set; }

        public bool IsAllocation { get; set; }

        public long JobEntryHeadID { get; set; }
        public new string ErrorCode { get; set; }
        public bool IsTransactionCompleted { get; set; }

        public Services.Contracts.Enums.DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }
    }
}