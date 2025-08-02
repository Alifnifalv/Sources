using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ServiceEntryMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Service Entry Details")]
    public class ServiceEntryMasterViewModel : BaseMasterViewModel
    {
        public ServiceEntryMasterViewModel()
        {
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.ServiceEntry;
            DeliveryType = new KeyValueViewModel();
            //Entitlements = new List<KeyValueViewModel>();
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType = new KeyValueViewModel();
            Currency = new KeyValueViewModel();
            //this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
        }

        public long TransactionHeadIID { get; set; }

        public String ReferenceTransactionHeaderID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[DisplayName("PO Number")]
        //[DataPicker("GRNAdvanceSearchView")]
        //public String ReferenceTransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false, optionalAttribute1: "ng-disabled='CRUDModel.Model.MasterViewModel.TransactionHeadIID !=0'")]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }
        public long BranchID { get; set; }

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
        [CustomDisplay("SENNo")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Validity")]
        public string Validity { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Currency")]
        [LookUp("LookUps.PurchaseCurrency")]
        [Select2("PurchaseCurrency", "Numeric", false, "OnPurchaseCurrencyChange($element)", false, optionalAttribute1: "ng-disabled=true")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-disabled=true")]
        [DisplayName("Exchange Rate")]
        public Nullable<decimal> ExchangeRate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Supplier")]
        //[Select2("Supplier", "Numeric", false)]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=Supplier", "LookUps.Supplier")]
        [Select2("SupplierID", "Numeric", false, "SupplierChangeOnReturn($event, $element); OnChangeSelect2", false)]
        [LookUp("LookUps.Supplier")]
        //[QuickSmartView("Supplier")]
        //[QuickCreate("Supplier")]
        public KeyValueViewModel Supplier { get; set; }
        public long SupplierID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DeliveryType", "Numeric")]
        //[DisplayName("Delivery Type")]
        [LookUp("LookUps.DeliveryType")]
        public KeyValueViewModel DeliveryType { get; set; }
        public int DeliveryTypeID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LookUp("LookUps.ReturnMethod")]
        //[CustomDisplay("ReturnMethod")]
        //[Select2("ReturnMethod", "Numeric")]
        public KeyValueViewModel ReturnMethod { get; set; }
        public string ReturnMethodID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("TransactionStatus", "Numeric")]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }

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

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Entitlement", "Numeric", true)]
        //[DisplayName("Entitlement")]
        //[LookUp("LookUps.Entitlement")]
        //public List<KeyValueViewModel> Entitlements { get; set; }
        public int EntitlementID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Shipment")]
        public bool IsShipment { get; set; }
        public int ShipmentID { get; set; }

        public bool IsTransactionCompleted { get; set; }

        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? LocalDiscount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}