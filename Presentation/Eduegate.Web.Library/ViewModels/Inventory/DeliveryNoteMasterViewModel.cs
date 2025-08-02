using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryNoteMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Delivery Details")]
    public class DeliveryNoteMasterViewModel : BaseMasterViewModel
    {
        public DeliveryNoteMasterViewModel()
        {
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.GoodsDeliveryNotes;
            //DeliveryDetails = new DeliveryAddressViewModel();
            DocumentStatus = new KeyValueViewModel();
            //this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
            TaxDetails = new TaxDetailsViewModel();
            DocumentType = new KeyValueViewModel();
        }

        public Nullable<long> ReferenceTransactionHeaderID { get; set; }

        [HasClaim(HasClaims.HASSALESORDERS)]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("From SO Number")]
        [DataPicker("SalesOrder")]
        public String ReferenceTransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")] //ng-disabled = 'CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null'
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
        [DisplayName("DN No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }      

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[DisplayName("Currency")]
        //[LookUp("LookUps.Currency")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        //public KeyValueViewModel Currency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Customer")]
        [Select2("Customer", "Numeric", false, "OnChangeSelect2")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.Customer")]
        //[QuickSmartView("Customer")]
        //[QuickCreate("Customer")]
        public KeyValueViewModel Customer { get; set; }
        public long SupplierID { get; set; }

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
        //[Select2("Entitlement", "Numeric", true)]
        //public List<KeyValueViewModel> Entitlements { get; set; }

        public int DeliveryTypeID { get; set; }
        public int EntitlementID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Sales man")]
        [LookUp("LookUps.SalesMan")]
        [HasClaim(HasClaims.HASSALESMAN)]
        public KeyValueViewModel SalesMan { get; set; }

        public long SalesManID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string Blank { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 750!")]
        public string Remarks { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Reference#")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Reference { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public long DocumentStatusID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "CRUDModel.Model.MasterViewModel.DeliveryDetails",
        //    ContainerAttribute = "ng-class={disabled:CRUDModel.Model.MasterViewModel.DeliveryMethod.Key==2}")]
        //[DisplayName("Delivery Details")]
        //[HasClaim(HasClaims.HASDELIVERY)]
        //public DeliveryAddressViewModel DeliveryDetails { get; set; }

        public string SearchText { get; set; }

        public Nullable<long> JobEntryHeadID { get; set; }

        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        public decimal DeliveryCharge { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool IsTransactionCompleted { get; set; }

        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public TaxDetailsViewModel TaxDetails { get; set; }

        public Nullable<int> CompanyID { get; set;}
    }
}