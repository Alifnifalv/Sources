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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Order Change Requests")]
    public class OrderChangeRequestMasterViewModel : BaseMasterViewModel
    {
        public OrderChangeRequestMasterViewModel()
        {
            DeliveryDetails = new DeliveryAddressViewModel();

            DeliveryType = new KeyValueViewModel();
            SalesMan = new KeyValueViewModel();
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.DataPicker, "onecol-header-left")]
        [DisplayName("OCR Number")]
        [DataPicker("ReplacementAdvanced", false, "ReplacementController")]
        public String ReferenceTransactionNo { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.la, "onecol-header-left", "ng-change='BranchChange($event, $element)'")]
        //[DisplayName("Branch")]
        //[LookUp("LookUps.Branch")]
        //public string Branch { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.Branch.Value'")]
        //[DisplayName("Branch")]
        //public string Branch { get; set; }
        public long BranchID { get; set; }

        public long TransactionHeadIID { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.DocumentType.Value'")]
        //[DisplayName("Document")]
        //[LookUp("LookUps.DocumentType")]
        //public string DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "")]
        [Select2("DocumentType", "String", false)]
        [DisplayName("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        public long DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("SO No")]
        public string TransactionNo { get; set; }

        public Nullable<long> ReferenceTransactionHeaderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.SalesMan.Value'")]
        //[Select2("Currency", "Numeric", false)]
        [DisplayName("Sales man")]
        [HasClaim(HasClaims.HASSALESMAN)]
        public KeyValueViewModel SalesMan { get; set; }

        public long SalesManID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.Currency.Value'")]
        //[Select2("Currency", "Numeric", false)]
        [DisplayName("Currency")]
        //[LookUp("LookUps.Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.Remarks'")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.Customer.Value'")]
        [DisplayName("Customer")]
        //[Select2("Customer", "Numeric", false, "OnChangeSelect2")]
        //[LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.Customer")]
        //[QuickSmartView("Customer")]
        [QuickCreate("Customer")]
        public KeyValueViewModel Customer { get; set; }


        public long CustomerID { get; set; }

        public string CustomerName { get; set; }

        public long SupplierID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.DeliveryType.Value'")]
        //[Select2("DeliveryType", "Numeric", false)]
        [DisplayName("Delivery Type")]
        //[LookUp("LookUps.DeliveryType")]
        public KeyValueViewModel DeliveryType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "ng-bind='CRUDModel.Model.MasterViewModel.DeliveryDate'")]
        [DisplayName("Delivery Date")]
        public string DeliveryDate { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Entitlement")]
        //[LookUp("LookUps.Entitlement")]
        //[Select2("Entitlements", "Numeric", true)]
        //public List<KeyValueViewModel> Entitlements { get; set; }

        public int DeliveryTypeID { get; set; }
        public byte EntitlementID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }

        

        public long DocumentStatusID { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "CRUDModel.Model.MasterViewModel.DeliveryDetails")]
        [DisplayName("Delivery Details")]
        public DeliveryAddressViewModel DeliveryDetails { get; set; }
        public bool IsTransactionCompleted { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }

        public Services.Contracts.Enums.DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }


    }
}
