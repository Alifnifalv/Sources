using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Domain;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PurchaseInvoiceMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Invoice Details")]
    public class PurchaseInvoiceMasterViewModel : BaseMasterViewModel
    {
        public PurchaseInvoiceMasterViewModel()
        {
            Statuses = new List<string>() { "New", "" };
            //Allocations = new BranchWiseAllocationViewModel();
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseInvoice;
            //DeliveryMethod = new KeyValueViewModel();
            //Entitlements = new List<KeyValueViewModel>();
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
            this.AdditionalExpTransMaps = new List<AdditionalExpTransMapViewModel>() { new AdditionalExpTransMapViewModel() };
            TaxDetails = new TaxDetailsViewModel();
            DocumentType = new KeyValueViewModel();
            Supplier = new KeyValueViewModel();
            Currency = new KeyValueViewModel();
        }

        public string ReferenceTransactionHeaderID { get; set; }
        public long? ReferenceHeadID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("")]
        public String space { get; set; }

        //[HasClaim(HasClaims.HASPURCHASEORDERS)]
        [ControlType(Framework.Enums.ControlTypes.DataPicker, "large-col-width")]
        [CustomDisplay("FromGRNNumber")]
        [DataPicker("GRNAdvanceSearch", true)]
        public String ReferenceGRNNo { get; set; }


        //[HasClaim(HasClaims.HASPURCHASEORDERS)]
        [ControlType(Framework.Enums.ControlTypes.DataPicker, "large-col-width")]
        [CustomDisplay("From PO Number")]
        //GRNAdvanceSearchView used as PO pick 
        [DataPicker("GRNAdvanceSearchView", true)]
        public String ReferencePONo { get; set; }

        //[HasClaim(HasClaims.HASPURCHASEORDERS)]
        [ControlType(Framework.Enums.ControlTypes.DataPicker, "large-col-width")]
        [CustomDisplay("FromSENNumber")]
        [DataPicker("ServiceEntryAdvanceSearch", true)]
        public String ReferenceSENNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [DisplayName("PO/GRN/SEN No")]
        public string ReferenceTransactionNo { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.ShowFlowStatus)]
        //[DisplayName("")]
        public List<string> Statuses { get; set; }

        public bool IsPick { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")] //ng-disabled = 'CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null'
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
        [DisplayName("PVR No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        //[HasClaim(HasClaims.HASPURCHASEORDERS)]
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[CustomDisplay("Validity")]
        public string Validity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Currency")]
        [LookUp("LookUps.PurchaseCurrency")]
        [Select2("PurchaseCurrency", "Numeric", false, "OnPurchaseCurrencyChange($element)", false)]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Supplier")]
        [Select2("Supplier", "Numeric", false, "OnChangeSelect2", false)]
        //[LazyLoad("", "Mutual/GetLazyLookUpData?lookType=Supplier", "LookUps.Supplier")]
        [LookUp("LookUps.Supplier")]
        //[QuickSmartView("Supplier")]
        //[QuickCreate("Supplier")]
        //[QuickCreate("Create,Frameworks/CRUD/Create?screen=Supplier, $event,Create Supplier, Supplier")]
        public KeyValueViewModel Supplier { get; set; }
        public long SupplierID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Exchange Rate")]
        public Nullable<decimal> ExchangeRate { get; set; }

        [HasClaim(HasClaims.HASREFERENCEFIELD)]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Reference#")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string Reference { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-change=GetExchangeRate($element)")]
        [DisplayName("Invoice Amount (Foreign)")]
        public Nullable<decimal> ForeignInvoiceAmount { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-change=GetExchangeRate($element)")]
        [DisplayName("Invoice Amount(Local)")]
        public Nullable<decimal> LocalInvoiceAmount { get; set; }



        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("DeliveryType", "Numeric")]
        //[CustomDisplay("DeliveryType")] 
        //[LookUp("LookUps.DeliveryType")]
        //[HasClaim(HasClaims.HASDELIVERY)]
        public KeyValueViewModel DeliveryMethod { get; set; }

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
        [CustomDisplay("DocumentStatus")]
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
        //[DisplayName("Shipment")]
        [HasClaim(HasClaims.HASSHIPMENT)]
        public bool IsShipment { get; set; }

        public int ShipmentID { get; set; }

        public string SearchText { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "Allocations", "CRUDModel.Model.MasterViewModel.Allocations.Allocations", "ng-disabled='!CRUDModel.Model.MasterViewModel.IsAllocation'")]
        //[DisplayName("Allocations")]
        //[HasClaim(HasClaims.HASALLOCATION)]
        //public BranchWiseAllocationViewModel Allocations { get; set; }
        public decimal? ForeignAmount { get; set; }
        public decimal? TotalLandingCost { get; set; }
        public decimal? LocalDiscount { get; set; }

        public bool IsAllocation { get; set; }

        public long JobEntryHeadID { get; set; }
        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }
        public bool IsTransactionCompleted { get; set; }

        public decimal DeliveryCharge { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        public List<AdditionalExpTransMapViewModel> AdditionalExpTransMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        
        //TODO: Need to check
        //[IgnoreMap]
        public TaxDetailsViewModel TaxDetails { get; set; }
        public string EmailID { get; set; }
        public Nullable<byte> SchoolID { get; set; }

    }
}