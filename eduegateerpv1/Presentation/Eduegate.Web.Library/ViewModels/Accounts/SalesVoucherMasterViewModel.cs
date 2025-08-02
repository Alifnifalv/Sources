using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SalesVoucherMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Order Details")]
    public class SalesVoucherMasterViewModel : BaseMasterViewModel
    {
        public SalesVoucherMasterViewModel()
        {
            DocumentStatus = new KeyValueViewModel();
            //this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
            TaxDetails = new TaxDetailsViewModel();
            DocumentType = new KeyValueViewModel();
        }

        public Nullable<long> ReferenceTransactionHeaderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")] //ng-disabled = 'CRUDModel.Model.MasterViewModel.ReferenceTransactionNo != null'
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false)]
        [DisplayName("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }

        public long BranchID { get; set; }

        public long TransactionHeadIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)")]
        [DisplayName("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }

        public int? DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("SV No")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Date")]
        public string TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Currency")]
        [LookUp("LookUps.Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Customer")]
        [Select2("Accounts", "Numeric", false)]
        [LookUp("LookUps.Accounts")]
        public KeyValueViewModel GLAccount { get; set; }

        public long SupplierID { get; set; }

        public int DeliveryTypeID { get; set; }
        public int EntitlementID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string Blank { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remarks")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        [Required]
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

        public string SearchText { get; set; }

        public Nullable<long> JobEntryHeadID { get; set; }

        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        public decimal DeliveryCharge { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool IsTransactionCompleted { get; set; }

        //public List<TransactionHeadEntitlementMapViewModel> TransactionHeadEntitlementMaps { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Payments")]
        public TaxDetailsViewModel TaxDetails { get; set; }

        public Nullable<int> CompanyID { get; set; }
    }
}
