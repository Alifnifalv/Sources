using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory.Purchase
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PurchaseQuotationMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Quotation Details")]
    public class PurchaseQuotationMasterViewModel : BaseMasterViewModel
    {
        public PurchaseQuotationMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            Tender = new KeyValueViewModel();
            //Supplier = new List<KeyValueViewModel> { new KeyValueViewModel() };
            //PurchaseRequests = new List<KeyValueViewModel> { new KeyValueViewModel() };
            DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.RFQ;
            SendMailNotification = false;
        }

        public string Screen { get; set; }
        public string Window { get; set; }

        public bool? ShowSaveButtons { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false)]
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
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        public long DocumentTypeID { get; set; }
        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("RFQ No")]
        public string TransactionNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Valid Date")]
        public string Validity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Currency")]
        [LookUp("LookUps.PurchaseCurrency")]
        [Select2("PurchaseCurrency", "Numeric", false, "OnPurchaseCurrencyChange($element)", false)]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-disabled=true")]
        [DisplayName("Exchange Rate")]
        public Nullable<decimal> ExchangeRate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Purchase Request")]
        [Select2("PurchaseRequests", "Numeric", true, "", false)]
        [LookUp("LookUps.PurchaseRequests")]
        public List<KeyValueViewModel> PurchaseRequests { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Supplier")]
        //[LazyLoad("", "Mutual/GetLazyLookUpData?lookType=Supplier", "LookUps.Supplier")]
        [LookUp("LookUps.Supplier")]
        [Select2("SupplierID", "Numeric", true, "", false)]
        [QuickSmartView("Supplier")]
        [QuickCreate("Supplier")]
        public List<KeyValueViewModel> SupplierList { get; set; } 
        public long SupplierID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        public string JobStatus { get; set; }
        public Nullable<int> JobStatusID { get; set; }

        public bool IsShipment { get; set; }

        public int ShipmentID { get; set; }

        public int EntitlementID { get; set; }

        public KeyValueViewModel TransactionStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        public Nullable<byte> TransactionStatusID { get; set; }
      

        public decimal DeliveryCharge { get; set; }
        public bool IsAllocation { get; set; }
        public bool IsTransactionCompleted { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }

        public Nullable<int> CompanyID { get; set; }

        public long? ReferenceHeadID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='GetProductItemsByRequestIds($event, $index, CRUDModel.Model.MasterViewModel)'")]
        [DisplayName("Fill product Items")]
        public string FillProductItems { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Tender", "Numeric", false)]
        [LookUp("LookUps.Tender")]
        [CustomDisplay("Tender")]
        public KeyValueViewModel Tender { get; set; }  
        
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Send Mail Notification")]
        public bool SendMailNotification { get; set; }
    }
}
