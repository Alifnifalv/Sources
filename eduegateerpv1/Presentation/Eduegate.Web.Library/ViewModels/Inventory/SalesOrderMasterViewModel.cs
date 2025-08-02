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
    [DisplayName("Order Details")]
    public class SalesOrderMasterViewModel : BaseMasterViewModel
    {
        public SalesOrderMasterViewModel()
        {
            //DeliveryDetails = new DeliveryAddressViewModel();
            //BillingDetails = new BillingAddressViewModel();
            //DeliveryPaymentDetail = new DeliveryPaymentDetailViewModel();
            //Entitlements = new List<KeyValueViewModel>();
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.SalesOrder;
            DeliveryMethod = new KeyValueViewModel();
            SalesMan = new KeyValueViewModel();
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType = new KeyValueViewModel();
            //this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
            Student = new KeyValueViewModel();
        }

        public string ReferenceTransactionHeaderID { get; set; }
        public long? ReferenceHeadID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("CHR Number")]
        [DataPicker("SalesQuotationAdvanceSearch")]
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
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)", optionalAttribute1: "ng-disabled=true")]
        [CustomDisplay("Document")]
        [LookUp("LookUps.DocumentType")]
        public KeyValueViewModel DocumentType { get; set; }

        public long DocumentTypeID { get; set; }

        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("SO No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalesMan", "Numeric", false)]
        [DisplayName("Sales man")]
        [LookUp("LookUps.SalesMan")]
        [HasClaim(HasClaims.HASSALESMAN)]
        public KeyValueViewModel SalesMan { get; set; }
        
        public long EmployeeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Currency")]
        [LookUp("LookUps.InventoryCurrency")]
        [Select2("InventoryCurrency", "Numeric", false, "OnPurchaseCurrencyChange($element)", false, optionalAttribute1: "ng-disabled='true'")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-blur=GetAllPurchaseLocalAmount($element)")]
        //[DisplayName("Exchange Rate")]
        public Nullable<decimal> ExchangeRate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Customer")]
        [Select2("Customer", "Numeric", false, "CustomerChange($event, $element); OnChangeSelect2", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.Student.Key")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.Customer")]
        //[QuickSmartView("Customer")]
        //[QuickCreate("Customer")]
        public KeyValueViewModel Customer { get; set; }
        public long CustomerID { get; set; }

        public long SupplierID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2/*, "onecol-header-left"*/)]
        [Select2("Student", "String", false, "ChangeInventoryStudent($event, $element,CRUDModel.Model.MasterViewModel)", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.Customer.Key")]
        [CustomDisplay("Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllStudents", "LookUps.AllStudents")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("ClassAndSection")]
        public string ClassSectionDescription { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("DeliveryType", "Numeric", false)]
        //[DisplayName("Delivery Type")]
        //[LookUp("LookUps.DeliveryType")]
        //[HasClaim(HasClaims.HASDELIVERY)]
        public KeyValueViewModel DeliveryMethod { get; set; }
         
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Delivery Date")]
        [HasClaim(HasClaims.HASDELIVERY)]
        public string DeliveryDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }

        [Required]
        [LookUp("LookUps.DocumentStatus")]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public long DocumentStatusID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("EntityType")]
        //[LookUp("LookUps.EntityTypes")]
        //[Select2("Entitlements", "Numeric", true)]
        //public KeyValueViewModel EntityType { get; set; }

        public int DeliveryTypeID { get; set; }
        public int EntitlementID { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "CRUDModel.Model.MasterViewModel.DeliveryDetails",
        //    ContainerAttribute = "ng-class={disabled:CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2}")]
        //[DisplayName("Delivery Details")]
        //public DeliveryAddressViewModel DeliveryDetails { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "BillingDetails", "CRUDModel.Model.MasterViewModel.BillingDetails",
        //    ContainerAttribute = "ng-class={disabled:CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2}")]
        //[DisplayName("Billing Details")]
        //public BillingAddressViewModel BillingDetails { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryPaymentDetail", "CRUDModel.Model.MasterViewModel.DeliveryPaymentDetail")]
        //[DisplayName("Delivery / Payment Detail")]
        //public DeliveryPaymentDetailViewModel DeliveryPaymentDetail { get; set; }

        public decimal DeliveryCharge { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool IsTransactionCompleted { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        ////[DisplayName("Payments")]
        //public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }

        public string EmailID { get; set; }
        public Nullable<byte> SchoolID { get; set; }

        public long SITransactionHeadID { get; set; }

    }
}