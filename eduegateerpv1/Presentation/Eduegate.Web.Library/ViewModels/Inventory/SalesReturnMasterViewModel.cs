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
    [DisplayName("Sales Return Details")]
    public class SalesReturnMasterViewModel : BaseMasterViewModel
    {
        public SalesReturnMasterViewModel()
        {
            //DeliveryDetails = new DeliveryAddressViewModel();
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.SalesReturn;
            DeliveryMethod = new KeyValueViewModel();
            SalesMan = new KeyValueViewModel(); 
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType = new KeyValueViewModel();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
        }
        public Nullable<long> ReferenceTransactionHeaderID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("SRR/SINumber")]
        [DataPicker("SalesReturnRequestAdvanceSearch")]
        public String ReferenceTransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false, optionalAttribute1: "ng-disabled='CRUDModel.Model.MasterViewModel.TransactionHeadIID !=0'")]
        [CustomDisplay("Branch")]
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


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("SRV No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [CustomDisplay("SalesMan")]
        [LookUp("LookUps.SalesMan")]
        [HasClaim(HasClaims.HASSALESMAN)]
        public KeyValueViewModel SalesMan { get; set; }

        public long SalesManID { get; set; }

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
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Customer")]
        [Select2("Customer", "Numeric", false, "OnChangeSelect2", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.Student.Key")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.Customer")]
        //[QuickSmartView("Customer")]
        //[QuickCreate("Customer")]
        public KeyValueViewModel Customer { get; set; }

        public long SupplierID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Student", "String", false, "", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.Customer.Key")]
        [CustomDisplay("Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllStudents", "LookUps.AllStudents")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("DeliveryType", "Numeric", false)]
        //[CustomDisplay("DeliveryType")]
        //[LookUp("LookUps.DeliveryType")]
        //[HasClaim(HasClaims.HASDELIVERY)]
        public KeyValueViewModel DeliveryMethod { get; set; } 

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("DeliveryDate")]
        [HasClaim(HasClaims.HASDELIVERY)]
        public string DeliveryDate { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Entitlement")]
        //[LookUp("LookUps.Entitlement")]
        //[Select2("Entitlements", "Numeric", true)]
        //public List<KeyValueViewModel> Entitlements { get; set; }

        public int DeliveryTypeID { get; set; }
        public int EntitlementID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Reference#")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Reference { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public long DocumentStatusID { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "CRUDModel.Model.MasterViewModel.DeliveryDetails", 
        //     ContainerAttribute = "ng-class={disabled:CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2}")]
        //[DisplayName("Delivery Details")]
        //public DeliveryAddressViewModel DeliveryDetails { get; set; }
        public bool IsTransactionCompleted { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? LocalDiscount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }

        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }
        public string EmailID { get; set; }
        public Nullable<byte> SchoolID { get; set; }
    }
}
