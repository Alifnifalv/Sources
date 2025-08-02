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
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SalesOrderMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Request Details")]
    public class SalesReturnRequestMasterViewModel : BaseMasterViewModel
    {
        public SalesReturnRequestMasterViewModel()
        {
            DeliveryDetails = new DeliveryAddressViewModel();
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.SalesReturnRequest;
            DeliveryMethod = new KeyValueViewModel();
            SalesMan = new KeyValueViewModel();
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            Student = new KeyValueViewModel();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
        }
        public Nullable<long> ReferenceTransactionHeaderID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("SI Number")]
        [DataPicker("SalesInvoiceAdvanceSearch")]
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
        [LookUp("LookUps.DocumentType")]
        [DisplayName("Document Type")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        public long DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("SRR No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Sales man")]
        [LookUp("LookUps.SalesMan")]
        [HasClaim(HasClaims.HASSALESMAN)]
        public KeyValueViewModel SalesMan { get; set; }

        public long SalesManID { get; set; }

   
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Currency")]
        [LookUp("LookUps.InventoryCurrency")]
        [Select2("InventoryCurrency", "Numeric", false, "OnPurchaseCurrencyChange($element)", false)]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-blur=GetAllPurchaseLocalAmount($element)")]
        [DisplayName("Exchange Rate")]
        public Nullable<decimal> ExchangeRate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Customer")]
        [Select2("Customer", "Numeric", false, "OnChangeSelect2")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.Customer")]
        [QuickSmartView("Customer")]
        [QuickCreate("Customer")]
        public KeyValueViewModel Customer { get; set; }

        public long SupplierID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Student", "String", false, "", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.Customer.Key")]
        [CustomDisplay("Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllStudents", "LookUps.AllStudents")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DeliveryType", "Numeric", false)]
        [DisplayName("Delivery Type")]
        [LookUp("LookUps.DeliveryType")]
        [HasClaim(HasClaims.HASDELIVERY)]
        public KeyValueViewModel DeliveryMethod { get; set; } 

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Delivery Date")]
        [HasClaim(HasClaims.HASDELIVERY)]
        public string DeliveryDate { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Entitlement")]
        //[LookUp("LookUps.Entitlement")]
        //[Select2("Entitlements", "Numeric", true)]
        //public List<KeyValueViewModel> Entitlements { get; set; }

        public int DeliveryTypeID { get; set; }
        public short EntitlementID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("TransactionStatus", "Numeric", false)]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "CRUDModel.Model.MasterViewModel.DeliveryDetails",
            ContainerAttribute = "ng-class={disabled:CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2}")]
        [DisplayName("Delivery Details")]
        public DeliveryAddressViewModel DeliveryDetails { get; set; }

        public bool IsTransactionCompleted { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }

        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }
    }
}
