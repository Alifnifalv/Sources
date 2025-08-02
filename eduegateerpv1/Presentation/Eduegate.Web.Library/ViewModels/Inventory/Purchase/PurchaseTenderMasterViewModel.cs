using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PurchaseTenderMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Tender Details")]
    public class PurchaseTenderMasterViewModel : BaseMasterViewModel
    {
        public PurchaseTenderMasterViewModel()
        {
            Document = new DocumentViewViewModel();

            DeliveryMethod = new KeyValueViewModel();
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();

            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false, optionalAttribute1: "ng-disabled='CRUDModel.Model.MasterViewModel.TransactionHeadIID !=0'")]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }
        public long BranchID { get; set; }

        public long TransactionHeadIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)", optionalAttribute1: "ng-disabled='CRUDModel.Model.MasterViewModel.TransactionHeadIID !=0'")]
        [DisplayName("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        public long DocumentTypeID { get; set; }
        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Tender No")]
        public string TransactionNo { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Validity")]
        public string Validity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Currency")]
        [LookUp("LookUps.Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Supplier")]
        //[Select2("Supplier", "Numeric", false)]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=Supplier", "LookUps.Supplier")]
        [Select2("SupplierID", "Numeric", false, "SupplierChange($event, $element); OnChangeSelect2", false)]
        [LookUp("LookUps.Supplier")]
        [QuickSmartView("Supplier")]
        [QuickCreate("Supplier")]
        public KeyValueViewModel Supplier { get; set; }
        public long SupplierID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Entitlement", "Numeric", true)]
        //[DisplayName("Entitlement")]
        //[LookUp("LookUps.Entitlement")]
        //public List<KeyValueViewModel> Entitlements { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DeliveryType", "Numeric")]
        // [DisplayName("Delivery Type")]
        [LookUp("LookUps.DeliveryType")]
        [HasClaim(HasClaims.HASDELIVERY)]
        public KeyValueViewModel DeliveryMethod { get; set; }

        public int DeliveryTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ReceivingMethod", "Numeric")]
        [LookUp("LookUps.ReceivingMethod")]
        [DisplayName("Receiving Method")]
        public KeyValueViewModel ReceivingMethod { get; set; }

        public string ReceivingMethodID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.JobStatuses")]
        //[DisplayName("JobStatus")]
        public string JobStatus { get; set; }
        public Nullable<int> JobStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Invoice Status")]
        public string InvoiceStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Delivery Date")]
        public string DeliveryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Does PO Have additional costs?")]
        public bool IsShipment { get; set; }

        public int ShipmentID { get; set; }

        public int EntitlementID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("TransactionStatus", "Numeric")]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public Nullable<byte> TransactionStatusID { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Documents", "Documents")]
        [DisplayName("Documents")]
        [LazyLoad("Mutual/DocumentFile", "Mutual/GetDocumentFiles", "CRUDModel.ViewModel.Document")]
        public DocumentViewViewModel Document { get; set; }

        public decimal DeliveryCharge { get; set; }
        public bool IsAllocation { get; set; }
        public bool IsTransactionCompleted { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }

        public Nullable<int> CompanyID { get; set; }

        public long? ReferenceHeadID { get; set; }
    }
}
