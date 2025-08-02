using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PurchaseReturnRequestMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Return Request Details")]
    public class PurchaseReturnRequestMasterViewModel : BaseMasterViewModel
    {
        public PurchaseReturnRequestMasterViewModel()
        {
            DeliveryType = new KeyValueViewModel();
            //Entitlements = new List<KeyValueViewModel>();
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
        }

        public String ReferenceTransactionHeaderID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("PI Number")]
        [DataPicker("PurchaseInvoiceAdvanceSearch")]
        public String ReferenceTransactionNo { get; set; }

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

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("PRR No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Validity")]
        public string Validity { get; set; }
        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

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
        [Select2("SupplierID", "Numeric", false, "OnChangeSelect2", false)]
        [LookUp("LookUps.Supplier")]
        [QuickSmartView("Supplier")]
        [QuickCreate("Supplier")]
        public KeyValueViewModel Supplier { get; set; }
        public long SupplierID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DeliveryType", "Numeric")]
        [DisplayName("Delivery Type")]
        [LookUp("LookUps.DeliveryType")]
        public KeyValueViewModel DeliveryType { get; set; }

        public int DeliveryTypeID { get; set; }

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

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Entitlement", "Numeric", true)]
        //[DisplayName("Entitlement")]
        //[LookUp("LookUps.Entitlement")]
        //public List<KeyValueViewModel> Entitlements { get; set; }

        public int EntitlementID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Shipment")]
        public bool IsShipment { get; set; }

        public int ShipmentID { get; set; }
        public bool IsTransactionCompleted { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
